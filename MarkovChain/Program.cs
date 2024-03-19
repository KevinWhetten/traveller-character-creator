// See https://aka.ms/new-console-template for more information

using MarkovChain;

var markovChain = new MarkovChain.MarkovChain(new LargosianSpelling());
markovChain.GeneratePlanetName("vargr_planets.txt");