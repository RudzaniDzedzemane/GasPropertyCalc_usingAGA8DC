namespace GasPropertyCalc
{
    public class GasProperty
    {
        /*
        =====================================================================
        === This opbject calculates properties of natural gas usuing the  ===
        === AGA8 detail characterisation method                           ===
        === Creeated by: R Dzedzemane
        === Initial revision: 11 January 2023                             ===
        =====================================================================
         */

        //define variables

        //gas composition order = [ methane, nitrogen, carbon dioxide, Ethane, propane, water, Hydrogen sulfide, Hydrogen, Carbon monoxide, 
        //                          Oxygen, iso-Butane, n-Butane, iso-Pentane, n-Pentane, n-Hexane, n-Heptane, n-Octane, n-Nonane, n-Decane, Helium, Argon ]

        private double[] gasComposition = { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        static readonly double R = 0.008314510;

        static readonly double[] an = { 0.153832600, 1.341953000, -2.998583000, -0.048312280, 0.375796500, -1.589575000, -0.053588470, 0.886594630, -0.710237040, -1.471722000,
                                        1.321850350, -0.786659250, 2.291290e-9, 0.157672400, -0.436386400, -0.044081590, -0.003433888, 0.032059050, 0.024873550, 0.073322790,
                                        -0.001600573, 0.642470600, -0.416260100, -0.066899570, 0.279179500, -0.696605100, -0.002860589, -0.008098836, 3.150547000, 0.007224479,
                                        -0.705752900, 0.534979200, -0.079314910, -1.418465000, -5.99905e-17, 0.105840200, 0.034317290, -0.007022847, 0.024955870, 0.042968180,
                                        0.746545300, -0.291961300, 7.294616000, -9.936757000, -0.005399808, -0.243256700, 0.049870160, 0.003733797, 1.874951000, 0.002168144,
                                        -0.658716400, 0.000205518, 0.009776195, -0.020487080, 0.015573220, 0.006862415, -0.001226752, 0.002850908};

        static readonly double[] bn = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3,
                                        3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 7, 7, 8, 8, 8, 9, 9};

        static readonly double[] cn = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0,
                                        1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1};

        static readonly double[] kn = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 2, 2, 2, 4, 4, 0, 0, 2, 2, 2, 4, 4, 4, 4,
                                        0, 1, 1, 2, 2, 3, 3, 4, 4, 4, 0, 0, 2, 2, 2, 4, 4, 0, 2, 2, 4, 4, 0, 2, 0, 2, 1, 2, 2, 2, 2};

        static readonly double[] un = { 0, 0.5, 1, 3.5, -0.5, 4.5, 0.5, 7.5, 9.5, 6, 12, 12.5, -6, 2, 3, 2, 2, 11, -0.5, 0.5,
                                        0, 4, 6, 21, 23, 22, -1, -0.5, 7, -1, 6, 4, 1, 9, -13, 21, 8, -0.5, 0, 2,
                                        7, 9, 22, 23, 1, 9, 3, 8, 23, 1.5, 5, -0.5, 4, 7, 3, 0, 1, 0};

        static readonly double[] gn = { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 1, 1,
                                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0};

        static readonly double[] qn = { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1,
                                        0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1};

        static readonly double[] fn = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1,
                                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

        static readonly double[] sn = { 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                        0, 0};

        static readonly double[] wn = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                        0, 0};

        static readonly double[] Mi = { 16.0430, 28.0135, 44.0100, 30.0700, 44.0970, 18.0153, 34.0820, 2.0159, 28.0100, 31.9988,
                                        58.1230, 58.1230, 72.1500, 72.1500, 86.1770, 100.2040, 114.2310, 128.2580, 142.2850, 4.0026, 39.9480};

        static readonly double[] Ei = { 151.318300, 99.737780, 241.960600, 244.166700, 298.118300, 514.015600, 296.355000, 26.957940, 105.534800, 122.766700,
                                        324.068900, 337.638900, 365.599900, 370.682300, 402.636293, 427.722630, 450.325022, 470.840891, 489.558373, 2.610111, 119.629900};

        static readonly double[] Ki = { 0.4619255, 0.4479153, 0.4557489, 0.5279209, 0.5837490, 0.3825868, 0.4618263, 0.3514916, 0.4533894, 0.4186954,
                                        0.6406937, 0.6341423, 0.6738577, 0.6798307, 0.7175118, 0.7525189, 0.7849550, 0.8152731, 0.8437826, 0.3589888, 0.4216551};

        static readonly double[] Gi = { 0, 0.027815, 0.189065, 0.079300, 0.141239, 0.332500, 0.088500, 0.034369, 0.038953, 0.021000,
                                        0.256692, 0.281835, 0.332267, 0.366911, 0.289731, 0.337542, 0.383381, 0.427354, 0.469659, 0, 0 };

        static readonly double[] Qi = { 0, 0, 0.690000, 0, 0, 1.067750, 0.633276, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        static readonly double[] Fi = { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        static readonly double[] Si = { 0, 0, 0, 0, 0, 1.582200, 0.390000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        static readonly double[] Wi = { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        // define binary interection matrices

        static readonly double[,] Eijstar = new double[21, 21] { {1, 0.971640, 0.960644, 1, 0.994635, 0.708218, 0.931484, 1.170520, 0.990126, 1, 1.019530, 0.989844,
                                                         1.002350, 0.999268, 1.107274, 0.880880, 0.880973, 0.881067, 0.881161, 1, 1 }, // 1: methane
                                                        {1, 1, 1.022740, 0.970120, 0.945939, 0.746954, 0.902271, 1.086320, 1.005710, 1.021000, 0.946914,
                                                         0.973384, 0.959340, 0.945520, 1, 1, 1, 1, 1, 1, 1},  // 2: nitrogen
                                                        {1, 1, 1, 0.925053, 0.960237, 0.849408, 0.955052, 1.281790, 1.500000, 1, 0.906849, 0.897362,
                                                         0.726255, 0.859764, 0.855134, 0.831229, 0.808310, 0.786323, 0.765171, 1, 1}, // 3: C02
                                                        {1, 1, 1, 1, 1.022560, 0.693168, 0.946871, 1.164460, 1, 1, 1, 1.013060, 1, 1.005320,
                                                         1, 1, 1, 1, 1, 1, 1}, // 4: ethane
                                                        {1, 1, 1, 1, 1, 1, 1, 1.034787, 1, 1, 1, 1.004900, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 5: Propane
                                                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 6: H20
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1.008692, 1.010126, 1.011501, 1.012821,
                                                        1.014089, 1, 1  }, // 7: Hydrogen Sulphide
                                                        { 1, 1, 1, 1, 1, 1, 1, 1, 1.100000, 1, 1.300000, 1.300000, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 8: Hydrogen
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 9
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 10
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 11
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 12
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 13
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 14
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 15
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 16
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 17
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 18
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 19
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 20
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } // 21
                                                        };


        static readonly double[,] Uij = new double[21, 21] {
                                                          { 1, 0.886106, 0.963827, 1, 0.990877, 1, 0.736833, 1.156390, 1, 1, 1, 0.992291,
                                                            1, 1.003670, 1.302576, 1.191904, 1.205769, 1.219634, 1.233498, 1, 1}, // 1: Methane
                                                          { 1, 1, 0.835058, 0.816431, 0.915502, 1, 0.993476, 0.408838, 1, 1, 1, 0.993556, 1, 1,
                                                            1, 1, 1, 1, 1, 1, 1}, // 2: Nitrogen
                                                          { 1, 1, 1, 0.969870, 1, 1, 1.045290, 1, 0.900000, 1, 1, 1, 1, 1, 1.066638, 1.077634,
                                                            1.088178, 1.098291, 1.108021, 1, 1}, // 3: Carbon dioxide
                                                          { 1, 1, 1, 1, 1.065173, 1, 0.971926, 1.616660, 1, 1, 1.250000, 1.250000, 1.250000,
                                                            1.250000, 1, 1, 1, 1, 1, 1, 1}, // 4: ethane
                                                           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 5: Propane
                                                          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, // 6: water
                                                          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1.028973, 1.033754, 1.038338,
                                                            1.042735, 1.046966, 1, 1 }, // 7: Hydrogen Sulphide 
                                                          {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 8
                                                          {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 9
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 10
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 11
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 12
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 13
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 14
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 15
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 16
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 17
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 18
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 19
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 20
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } // 21
                                                           };

        static readonly double[,] Kij = new double[21, 21]   {
                                                          { 1, 1.003630, 0.995933, 1, 1.007619, 1, 1.000080, 1.023260, 1, 1, 1, 0.997596, 1,
                                                           1.002529, 0.982962, 0.983565, 0.982707, 0.981849, 0.980991, 1, 1}, // 1: Methane
                                                          { 1, 1, 0.982361, 1.007960, 1, 1, 0.942596, 1.032270, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, // 2: Nitrogen
                                                          { 1, 1, 1, 1.008510, 1, 1, 1.007790, 1, 1, 1, 1, 1, 1, 1, 0.910183, 0.895362, 0.881152,
                                                            0.867520, 0.854406, 1, 1}, // 3: C02
                                                          { 1, 1, 1, 1, 0.986893, 1, 0.999969, 1.020340, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, // 4: ethane
                                                          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, // 5: Propane
                                                          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, // 6: Water
                                                          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0.968130, 0.962870, 0.957828, 0.952441, 0.948338, 1, 1 }, //7: Hydrogen Sulphide
                                                          {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 8
                                                          {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 9
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 10
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 11
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 12
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 13
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 14
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 15
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 16
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 17
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 18
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 19
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 20
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } // 21
                                                            };

        static readonly double[,] Gijstar = new double[21, 21] {
                                                         { 1, 1, 0.807653, 1, 1, 1, 1, 1.957310, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, }, // 1: Methane
                                                         { 1, 1, 0.982746, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,}, // 2: Nitrogen
                                                         { 1, 1, 1, 0.370296, 0, 1.673090, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, // 3: C02
                                                         {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 8
                                                          {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 9
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 10
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 11
                                                          {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 8
                                                          {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 9
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 10
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 11
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 12
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 13
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 14
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 15
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 16
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 17
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 18
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 19
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 20
                                                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } // 21
                                                         };

        //define constructor

        public GasProperty()
        {

        }

        public GasProperty(double[] gasComposition)
        {
            this.gasComposition = gasComposition;
        }

        public void printValues()
        {
            Console.WriteLine("Number of values in an is: " + an.Length);
        }

        //====================================================================
        //== interface methods                                            ====
        //====================================================================

        public double getZ(double T, double P)
        {
            double rhom = 0;

            double Pest;

            double Z;

            double[] normalizedComp = normalizeInputComposition(gasComposition);

            double PMpa, Tkal;

            //perform unit conversions

            Tkal = T + 273.15;

            PMpa = P / 1000;

            rhom = getrhom(normalizedComp, Tkal, PMpa);

            Pest = getPest(normalizedComp, Tkal, rhom);

            Z = PMpa / (rhom * R * Tkal);

            return Z;
        }

        public double getrho(double T, double P)
        {
            double rho = 0;

            double rhom;

            double[] normalizedComp = normalizeInputComposition(gasComposition);

            double PMpa, Tkal;

            double M = 0;

            //perform unit conversions

            Tkal = T + 273.15;

            PMpa = P / 1000;

            rhom = getrhom(normalizedComp, Tkal, PMpa);

            //get molar weight

            for (int i = 0; i < normalizedComp.Length; i++)
            {
                M = M + normalizedComp[i] * Mi[i];
            }

            rho = M * rhom;

            return rho;
        }

        //method used to normalize the input gas composition to ensure sum is 1

        private double[] normalizeInputComposition(double[] inComposition)
        {
            double[] NormalizedComposition = new double[this.gasComposition.Length];

            double CompSum = 0;

            int N = this.gasComposition.Length;

            int i;

            for (i = 0; i < N; i++)
            {
                CompSum = CompSum + this.gasComposition[i];
            }

            for (i = 0; i < N; i++)
            {
                NormalizedComposition[i] = this.gasComposition[i] / CompSum;
            }

            return NormalizedComposition;
        }

        //===================================================================
        //== This method estimates the molar density of the gas given    ====
        //== the input composition x, pressure P (Mpa) and temperature   ====
        //== T in kalvin using the secant method                         ====
        //===================================================================

        private double getrhom(double[] x, double T, double P)
        {
            double rhom = 0;

            double fxn;

            int i = 0;

            double xn, xnMinusOne, xnMinusTwo;

            double xlimit = 1e-10;

            double flimit = 1e-20;

            double maxIter = 100;

            xnMinusOne = P / (R * T);

            xnMinusTwo = xnMinusOne * 1.4;

            do
            {
                xn = xnMinusOne - (P - getPest(x, T, xnMinusOne)) * ((xnMinusOne - xnMinusTwo) / ((P - getPest(x, T, xnMinusOne)) - (P - getPest(x, T, xnMinusTwo))));

                fxn = P - getPest(x, T, xn);

                xnMinusTwo = xnMinusOne;

                xnMinusOne = xn;

                i = i + 1;

            } while ((Math.Abs(fxn) > flimit) && (Math.Abs(xnMinusOne - xnMinusTwo) > xlimit) && (i < maxIter));

            rhom = xn;

            return rhom;
        }

        //===================================================================
        //=== This method estimates the system pressure P (Mpa) based on  ===
        //=== an estimate of the molar density rhom, composition x and    ===
        //=== input tempetature T (kalvin)                                ===
        //===================================================================

        private double getPest(double[] x, double T, double rhom)
        {
            double Pest = 0;

            double B = getB(T, x);

            double K = getK(x);

            double sum1 = 0;

            double sum2 = 0;

            double rhor = K * K * K * rhom;

            //sum for Cnstar

            for (int n = 12; n < 18; n++)
            {
                sum1 = sum1 + getCnstar(T, x, n);
            }

            for (int n = 12; n < 58; n++)
            {
                sum2 = sum2 + getCnstar(T, x, n) * (bn[n] - cn[n] * kn[n] * Math.Pow(rhor, kn[n])) * Math.Pow(rhor, bn[n]) * Math.Exp(-1 * cn[n] * Math.Pow(rhor, kn[n]));
            }

            Pest = rhom * R * T * (1 + B * rhom - rhor * sum1 + sum2);

            return Pest;
        }

        //===================================================================
        //=== This method calculates the coeficient Cnstar                 ==
        //=== Unit of absolute temperature T is Kalvin                     ==
        //===================================================================

        private double getCnstar(double T, double[] x, int n)
        {
            double Cnstar = 0;

            double G = getG(x);

            double Q = getQ(x);

            double F = getF(x);

            double U = getU(x);

            Cnstar = an[n] * Math.Pow(G + 1 - gn[n], gn[n]) * Math.Pow(Q * Q + 1 - qn[n], qn[n]) * Math.Pow(F + 1 - fn[n], fn[n]) * Math.Pow(U, un[n]) * Math.Pow(T, -1 * un[n]);

            return Cnstar;
        }

        //==============================================================
        //=== This method calculates the second virial coeficient B   ==
        //=== Unit of absolute temperature T is Kalvin                ==
        //==============================================================


        private double getB(double T, double[] x)
        {
            double B = 0;

            double[,] Gij = getGij();

            double[,] Eij = getEij();

            int N = x.Length;

            double Bnijstar;

            double sum1 = 0;

            double temp;

            for (int n = 0; n < 18; n++)
            {
                //calculate the value over i and j

                temp = 0;

                for (int i = 0; i < N; i++)
                {

                    for (int j = 0; j < N; j++)
                    {
                        Bnijstar = Math.Pow(Gij[i, j] + 1 - gn[n], gn[n]) * Math.Pow(Qi[i] * Qi[j] + 1 - qn[n], qn[n]) * Math.Pow(Math.Sqrt(Fi[i]) * Math.Sqrt(Fi[j]) + 1 - fn[n], fn[n]) *
                                    Math.Pow(Si[i] * Si[j] + 1 - sn[n], sn[n]) * Math.Pow(Wi[i] * Wi[j] + 1 - wn[n], wn[n]);

                        temp = temp + x[i] * x[j] * Bnijstar * Math.Pow(Eij[i, j], un[n]) * Math.Pow(Ki[i] * Ki[j], 1.5);
                               
                    }
                }

                sum1 = sum1 + an[n] * Math.Pow(T, -1 * un[n]) * temp;
            }

            B = sum1;

            return B;
        }

        private double getK(double[] x)
        {
            double K = 0;

            int N = x.Length;

            double sum1 = 0;

            double sum2 = 0;

            for (int i = 0; i < N; i++)
            {
                sum1 = sum1 + x[i] * Math.Pow(Ki[i], 2.5);
            }

            sum1 = sum1 * sum1;

            for (int i = 0; i < N - 1; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    sum2 = sum2 + x[i] * x[j] * (Math.Pow(Kij[i, j], 5) - 1) * Math.Pow(Ki[i] * Ki[j], 2.5);
                }
            }

            K = sum1 + 2 * sum2;

            K = Math.Pow(K, 0.2);


            return K;
        }

        private double getF(double[] x)
        {
            double F = 0;

            for (int i = 0; i < x.Length; i++)
            {
                F = F + x[i] * x[i] * Fi[i];
            }

            return F;

        }

        private double getQ(double[] x)
        {
            double Q = 0;

            for (int i = 0; i < x.Length; i++)
            {
                Q = Q + x[i] * Qi[i];
            }

            return Q;
        }

        private double getG(double[] x)
        {
            double Gout = 0;

            double sum1 = 0;

            double sum2 = 0;

            int N = x.Length;

            for (int i = 0; i < N; i++)
            {
                sum1 = sum1 + x[i] * Gi[i];
            }

            //for sum2 

            for (int i = 0; i < N - 1; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    sum2 = sum2 + x[i] * x[j] * (Gijstar[i, j] - 1) * (Gi[i] + Gi[j]);
                }
            }

            Gout = sum1 + 2 * sum2;

            return Gout;
        }

        private double getU(double[] x)
        {
            double U = 0;

            double sum1 = 0;

            double sum2 = 0;

            int N = x.Length;

            for (int i = 0; i < x.Length; i++)
            {
                sum1 = sum1 + x[i] * Math.Pow(Ei[i], 2.5);
            }

            sum1 = sum1 * sum1;

            for (int i = 0; i < N - 1; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    sum2 = sum2 + x[i] * x[j] * (Math.Pow(Uij[i, j], 5) - 1) * Math.Pow((Ei[i] * Ei[j]), 2.5);
                }
            }

            U = sum1 + 2 * sum2;

            U = Math.Pow(U, 0.2);

            return U;
        }

        private double[,] getEij()
        {
            int N = Ei.Length;

            double[,] Eij = new double[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Eij[i, j] = Eijstar[i, j] * Math.Pow((Ei[i] * Ei[j]), 0.5);
                }

            }

            return Eij;
        }

        private double[,] getGij()
        {
            int N = Gi.Length;

            double[,] Gij = new double[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Gij[i, j] = (Gijstar[i, j] * (Gi[i] + Gi[j])) / 2;
                }

            }

            return Gij;
        }



    }
}
