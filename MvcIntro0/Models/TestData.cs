using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcIntro0.Models
{
    public class TestData
    {
        public static void Initialize(StoreContext cntxt)
        {
            if (cntxt.Bikes.Any())
            {
                return;
            }
            cntxt.AddRange(
                new Bike { Line= "Domane", Model= "SL6", Frame= "500 Series OCLV", Fork= "Domane SLR carbon", Shifter= "Shimano Ultegra", Brake= "Shimano Ultegra", Cost=4100},
                new Bike { Line= "Madone", Model= "SL7", Frame= "500 Series OCLV", Fork= "Madone KVF full carbon", Shifter= "Shimano Ultegra Di2", Brake= "Shimano Ultegra", Cost=6500},
                new Bike { Line= "FX", Model= "Sport 6", Frame= "400 series OCLV carbon", Fork= "FX Carbon", Shifter= "Shimano RS700", Brake= "Tektro HD-R310", Cost=2200},
                new Bike { Line= "Roscoe", Model= "8", Frame= "Alpha Gold Aluminum", Fork= "RockShox 35 Gold RL", Shifter= "SRAM NX Eagle", Brake= "Shimano", Cost=1880},
                new Bike { Line= "Checkpoint", Model= "ALR4", Frame= "300 Series Alpha Aluminum", Fork= "Checkpoint carbon", Shifter= "Shimano GRX RX400", Brake= "Shimano RX400", Cost=1700},
                new Bike { Line= "Rail 9.9", Model= "X01 AXS", Frame= "OCLV Mountain", Fork= "RockShox ZEB Ultimate", Shifter= "SRAM Eagle AXS", Brake= "SRAM Code RSC", Cost=13000},
                new Bike { Line= "E-Caliber 9.9", Model= "XX1 AXS", Frame= "OCLV Mountain", Fork= "RockShox SID Ultimate", Shifter= "SRAM Eagle AXS", Brake= "Shimano XTR M9120 4-piston", Cost=13000},
                new Bike { Line= "Domane+", Model= "LT9", Frame= "500 Series OCLV", Fork= "Domane SL carbon", Shifter= "Shimano Dura-Ace Di2 R9170", Brake= "Shimano Dura-Ace", Cost=12500},
                new Bike { Line= "Émonda", Model= "SLR 9", Frame= "Ultralight 800 Series OCLV Carbon", Fork= "Emonda SLR full carbon", Shifter= "Shimano Dura-Ace Di2 R9170", Brake= "Shimano Dura-Ace", Cost=12500},
                new Bike { Line= "Madone", Model= "SLR 9", Frame= "800 Series OCLV Carbon", Fork= "Madone KVF full carbon", Shifter= "Shimano Dura-Ace Di2 R9170", Brake= "Shimano Dura-Ace", Cost=12500},
                new Bike { Line= "Madone", Model= "SLR 9 eTap", Frame= "800 Series OCLV Carbon", Fork= "Madone KVF full carbon", Shifter= "SRAM RED eTap AXS", Brake= "SRAM Red eTap AXS", Cost=12500},
                new Bike { Line= "Rail 9.9", Model= "XTR", Frame= "OCLV Mountain Carbon main frame", Fork= "RockShox ZEB Ultimate", Shifter= "Shimano XTR M9100", Brake= "Shimano XTR M9120 4-piston", Cost=12000},
                new Bike { Line= "E-Caliber 9.9", Model= "XTR", Frame= "OCLV Mountain Carbon main frame & stays", Fork= "Fox Factory 34 Step-Cast", Shifter= "Shimano XTR M9100", Brake= "Shimano XTR M9120 4-piston", Cost=12000},
                new Bike { Line= "Rail 9.9", Model= "", Frame= "OCLV Mountain Carbon main frame", Fork= "RockShox ZEB Ultimate", Shifter= "SRAM X01 Eagle", Brake= "SRAM Code RSC", Cost=11000},
                new Bike { Line= "Supercaliber 9.9", Model= "XX1 AXS", Frame= "OCLV Mountain Carbon main frame & stays", Fork= "RockShox SID SL Ultimate", Shifter= "SRAM Eagle AXS", Brake= "SRAM Level Ultimate", Cost=11000},
                new Bike { Line= "Top Fuel 9.9", Model= "XX1 AXS", Frame= "OCLV Mountain Carbon main frame & stays", Fork= "RockShox SID Ultimate", Shifter= "SRAM Eagle AXS", Brake= "SRAM G2 Ultimate", Cost=10500},
                new Bike { Line= "Domane+", Model= "HP7", Frame= "500 Series OCLV Carbon", Fork= "Domane+ carbon", Shifter= "Shimano Ultegra Di2", Brake= "Shimano RX400", Cost=9700},
                new Bike { Line= "Slash 9.9", Model= "XTR", Frame= "OCLV Mountain Carbon main frame & stays", Fork= "RockShox ZEB Ultimate", Shifter= "Shimano XTR M9100", Brake= "Shimano XTR M9120 4-piston", Cost=10000},
                new Bike { Line= "Supercaliber 9.9", Model= "XX1", Frame= "OCLV Mountain Carbon main frame & stays", Fork= "RockShox SID SL Ultimate", Shifter= "SRAM XX1 Eagle", Brake= "SRAM Level Ultimate", Cost=10000},
                new Bike { Line= "E-Caliber 9.8", Model= "GX AXS", Frame= "OCLV Mountain Carbon main frame & stays", Fork= "RockShox SID Select+", Shifter= "SRAM GX Eagle AXS", Brake= "SRAM G2 RSC 4-piston", Cost=9200},
                new Bike { Line= "Domane+", Model= "LT7", Frame= "500 Series OCLV", Fork= "Domane SL carbon", Shifter= "Shimano Ultegra Di2", Brake= "Shimano Ultegra", Cost=9200},
                new Bike { Line= "Supercaliber 9.9", Model= "XTR", Frame= "OCLV Mountain Carbon main frame & stays", Fork= "RockShox SID SL Ultimate", Shifter= "Shimano XTR M9100", Brake= "Shimano XTR M9100", Cost=9500},
                new Bike { Line= "Madone", Model= "SLR 7 eTap", Frame= "800 Series OCLV Carbon", Fork= "Madone KVF full carbon", Shifter= "SRAM Force eTap AXS", Brake= "SRAM Force eTap AXS", Cost=9000},
                new Bike { Line= "Fuel EX 9.9", Model= "X01 AXS", Frame= "OCLV Mountain Carbon main frame & stays", Fork= "Fox Factory 36", Shifter= "SRAM Eagle AXS", Brake= "SRAM G2 Ultimate", Cost=9500},
                new Bike { Line= "Rail 9.8", Model="", Frame= "OCLV Mountain Carbon main frame", Fork= "RockShox ZEB Select+", Shifter= "SRAM GX Eagle", Brake= "SRAM Code R 4-piston", Cost=8500},
                new Bike { Line= "E-Caliber 9.8", Model= "XT", Frame= "OCLV Mountain Carbon main frame & stays", Fork= "RockShox SID Select+", Shifter= "Shimano XT M8100", Brake= "Shimano XT M8120 4-piston", Cost=8500},
                new Bike { Line= "E-Caliber 9.8", Model= "GX", Frame= "OCLV Mountain Carbon main frame & stays", Fork= "RockShox SID Select+", Shifter= "SRAM GX Eagle", Brake= "SRAM G2 RSC 4-piston", Cost=8500},
                new Bike { Line= "Rail 9.8", Model= "XT", Frame= "OCLV Mountain Carbon main frame", Fork= "RockShox ZEB Select+", Shifter= "Shimano XT M8100", Brake= "Shimano SLX M7120 4-piston", Cost=8500},
                new Bike { Line= "Top Fuel 9.9", Model= "XTR", Frame= "OCLV Mountain Carbon main frame & stays", Fork= "Fox Factory 34 Step-Cast", Shifter= "Shimano XTR M9100", Brake= "Shimano XTR M9100", Cost=9000},
                new Bike { Line= "Émonda", Model= "SLR 7", Frame= "Ultralight 800 Series OCLV Carbon", Fork= "Emonda SLR full carbon", Shifter= "Shimano Ultegra Di2", Brake= "Shimano Ultegra", Cost=8800}
                );

            cntxt.SaveChanges();
        }
    }
}
