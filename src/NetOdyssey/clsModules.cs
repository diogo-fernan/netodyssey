using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Security.Permissions;

namespace NetOdyssey
{
	abstract class clsModules
	{
		static String[] _referenceAssemblies = { "System.dll", "SharpPcap.dll", "NetOdysseyModule.dll", "PacketDotNet.dll" };

		/// <summary>
		/// Compiles all *.cs and *.vb files in the specified directory.
		/// </summary>
		/// <param name="inSourceDirectory">The directory where the modules to be compiled are.</param>
		/// <returns>True if errors occurred while compiling, false otherwise.</returns>
		public static bool compileModules(DirectoryInfo inSourceDirectory)
		{
			bool _errorsOccured = false;
			CSharpCodeProvider _CSCodeProvider;
			VBCodeProvider _VBCodeProvider;
			CompilerParameters _compilerParameters;
			CompilerResults _moduleCompileResults;
			_CSCodeProvider = new CSharpCodeProvider();
			_VBCodeProvider = new VBCodeProvider();            
			foreach (FileInfo sourceFile in inSourceDirectory.GetFiles())
			{
				if (sourceFile.Extension == ".cs" || sourceFile.Extension == ".vb")
				{
					clsMessages.PrintCompilerMessage("Compiling " + sourceFile.Name);
					System.Windows.Forms.TreeNode rootNode = new System.Windows.Forms.TreeNode();
					rootNode.Text = sourceFile.Name;
					_compilerParameters = new CompilerParameters(_referenceAssemblies, sourceFile.FullName + ".netOdysseyModule") { GenerateExecutable = false, GenerateInMemory = true};
					if (sourceFile.Extension == ".cs") 
						_moduleCompileResults = _CSCodeProvider.CompileAssemblyFromFile(_compilerParameters, sourceFile.FullName);
					else if (sourceFile.Extension == ".vb") {
						_compilerParameters.OutputAssembly += ".dll";
						_moduleCompileResults = _VBCodeProvider.CompileAssemblyFromFile(_compilerParameters, sourceFile.FullName);                     
					}
					else throw new Exception("Unknown module extension: " + sourceFile.Extension);

					if (_moduleCompileResults.Errors.Count > 0)
					{
						rootNode.ImageIndex = 1;
						foreach (CompilerError compileError in _moduleCompileResults.Errors)
						{
							string compilerMessage = sourceFile.Name;
							System.Windows.Forms.TreeNode errorNode = new System.Windows.Forms.TreeNode();
							if (compileError.IsWarning) 
								errorNode.ImageIndex = 1;
							else
							{
								errorNode.ImageIndex = 2;
								rootNode.ImageIndex = 2;
								_errorsOccured = true;
							}
							errorNode.Text = compileError.ErrorNumber + ": " + compileError.ErrorText;
							clsMessages.PrintCompilerMessage("E " + errorNode.Text + " | Line: " + compileError.Line + " Column: " + compileError.Column);
							errorNode.Nodes.Add(new System.Windows.Forms.TreeNode("Line: " + compileError.Line + " Column: " + compileError.Column) { ImageIndex = 3 });
							rootNode.Nodes.Add(errorNode);
						}
					}
					else
					{
						bool isINetOdysseyModule = false;
						//bool wasIgnored = false;

						Assembly module = _moduleCompileResults.CompiledAssembly;
						foreach (Type t in module.GetTypes())
						{
							if (t.IsClass && t.IsSubclassOf(typeof(NetOdysseyModule.NetOdysseyModuleBase)))
							{                                
								isINetOdysseyModule = true;
								// NetOdysseyModule.NetOdysseyModuleBase moduleInstance = (NetOdysseyModule.NetOdysseyModuleBase) Activator.CreateInstance(t);
								// moduleInstance.prpModuleName = sourceFile.Name;                                
								// moduleInstance.prpReportFolder = Program.prpSettings.ReportsFolder;
								

								// if ((Program.prpSettings.CaptureMode == CaptureMode.Packets && !(typeof(NetOdysseyModule.INetOdysseyPacketAnalyzerModule).IsAssignableFrom(t.))) ||
								//     (Program.prpSettings.CaptureMode == CaptureMode.Statistics && !(typeof(NetOdysseyModule.INetOdysseyBCTUAnalyzerModule).IsAssignableFrom(t))))
								//     wasIgnored = true;
								// else {
									// prpModules.Add(moduleInstance);


								// Save the type of the module and its file name
								Program.prpModulesTypeList.Add(t);
								Program.prpModulesNameList.Add(sourceFile.Name);


								//     wasIgnored = false;
								// }
							}
						}

						if (isINetOdysseyModule)
						{
							// if (wasIgnored) {
							//     rootNode.Nodes.Add(new System.Windows.Forms.TreeNode("This module was ignored because of the analysis settings.") { ImageIndex = 1 });
							//     rootNode.ImageIndex = 1;
							// } else {
								clsMessages.PrintCompilerMessage(sourceFile.Name + " compiled with no errors.");
								rootNode.ImageIndex = 0;
							// }
						}
						else
						{
							clsMessages.PrintCompilerMessage(sourceFile.Name + " compiled with no errors, but does not implement any module interface.");
							rootNode.Nodes.Add(new System.Windows.Forms.TreeNode("Does not implement any module interface") { ImageIndex = 2 });
							rootNode.ImageIndex = 2;
							_errorsOccured = true;
						}
					}
					Program.prpFrmModuleCompiler.trvCompileResults.Nodes.Add(rootNode);
				}
			}
			Program.prpFrmModuleCompiler.trvCompileResults.ExpandAll();
			Program.prpSettings.createReportsFolder();
			return _errorsOccured;
		}
	}
}
