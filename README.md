# NetOdyssey

*NetOdyssey* is a modular, multi-threaded tool developed in C# a few years back by a group of researchers with the main goal of providing statistical estimations of network traffic in real-time by using a sliding window in a packet-by-packet or a flow-by-flow manner. Statistical analysis of network traffic is an alternative to deep packet inspection (DPI). It is nowadays an active research area due to the evasive nature of many applications and network communications. The tool was designed with a friendly modular interface for writing customized statistical modules (*e.g.*, entropy, autocorrelation, moving average, or the Hurst parameter) with ease. The interface provides generic methods to start up a statistical module, analyze packets or flows, report analyzes, and end the statistical module. Each module runs its on its own thread and individually reports results to a pre-specified file. *NetOdyssey* depicts a preferences pane, allowing the specification of filters and the type of flows to consider (*e.g.*, unidirectional or bidirectional). A few statistical modules focusing on the analysis of packets or flows are already bundled with the tool.

*NetOdyssey* has been written for researchers and for research purposes. With that in mind, the tool can be used for specific network traffic analyzes scenarios needing statistical information of the traffic under observation.

# Dependencies

*NetOdyssey* is provided as a Microsoft Visual Studio project and depends of the following to capture network packets in promiscuous mode:

* [WinPcap](http://www.winpcap.org/), a popular packet capture library; and
* [SharpPcap](http://sourceforge.net/projects/sharppcap/), a cross-platform packet capture framework for the .NET environment based on WinPcap.

# References and Materials

For detailed information about *NetOdyssey*, please refer to the following publications:

* Fábio D. Beirão. [*NetOdyssey: A Framework for Real-Time Windowed Analysis of Network Traffic*](http://www.di.ubi.pt/~mario/files/MScDissertation-FabioBeirao.pdf). M.Sc. Dissertation, University of Beira Interior, Rua Marquês d’Ávila e Bolama, 6201-001 Covilhã, Portugal, 2010.
* Fábio D. Beirão, João V. Gomes, Pedro R. M. Inácio; Manuela Pereira, and Mário M. Freire, [*NetOdyssey - A New Tool for Real-Time Analysis of Network Traffic*](http://ieeexplore.ieee.org/xpl/articleDetails.jsp?arnumber=5598201). In Proc. of the 9th IEEE Int. Symp. on Network Computing and Applications (NCA), pages 239-242, Cambridge, MA USA, Jul. 15-17 2010. IEEE.
* Liliana F. B. Soares, Diogo A. B. Fernandes, João V. Gomes, Manuela Pereira, Mário M. Freire, and Pedro R. M. Inácio. [*NetOdyssey: A Flexible Tool for Real-Time Statistical Analysis of Network Traffic Flows*](https://www.researchgate.net/publication/260134640_NetOdyssey_A_Flexible_Tool_for_Real-Time_Statistical_Analysis_of_Network_Traffic_Flows). In Atas da 11a Conferência sobre Redes de Computadores (CRC 2011), pages 135-143, Coimbra, Portugal, Nov. 17-18 2013. UC. Acceptance ratio: 27%.
