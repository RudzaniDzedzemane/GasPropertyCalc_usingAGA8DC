// See https://aka.ms/new-console-template for more information

//gas order = [ methane, nitrogen, carbon dioxide, Ethane, propane, water, Hydrogen sulfide, Hydrogen, Carbon monoxide, 
//              Oxygen, iso-Butane, n-Butane, iso-Pentane, n-Pentane, n-Hexane, n-Heptane, n-Octane, n-Nonane, n-Decane, Helium, Argon ]

double[] inputComposition = { 0.965, 0.003, 0.006, 0.018, 0.0045, 0, 0, 0, 0, 0, 0.0010, 0.0010, 0.0005, 0.0003, 0.0007, 0, 0, 0, 0, 0, 0 };

GasPropertyCalc.GasProperty gas = new GasPropertyCalc.GasProperty(inputComposition);

double P = 12000;

double T = 36.85;

double Z = gas.getZ(T, P);

double rho = gas.getrho(T, P);



Console.WriteLine("For pressurer " + P + " and temperature " + T);

Console.WriteLine("Calculated value of Z is " + Z);

Console.WriteLine("Calculated value of density is " + rho);
