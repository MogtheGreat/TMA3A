using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMA3A.part3.computerParts
{
    public class MasterList
    {
        /*
         * This class is used to init and hold all component objects for part 3. 
         * Stores reference to these objects in cart list.
         * The only new objects that are created outside of this class are custom computer object on the Customize.aspx page.
         * Component object could be made into const?
         */
        public static Ram ramOne = new Ram("Corsair", 84.99, "16GB");
        public static Ram ramTwo = new Ram("Timetec Hynix IC", 81.99, "8GB");
        public static Ram ramThree = new Ram ("G.SKILL", 292.82, "32GB");

        public static CPU cpuOne = new CPU ("intel", 429.99, 8);
        public static CPU cpuTwo = new CPU ("AMD", 208.37, 6);
        public static CPU cpuThree = new CPU("AMD", 621.48, 12);

        public static HardDrive hardOne = new HardDrive ("Seagate", 74.99, "2TB");
        public static HardDrive hardTwo = new HardDrive ("Western Digital", 114.99, "4TB");
        public static HardDrive hardThree = new HardDrive ("Toshiba", 272.66, "8TB");

        public static SoundCard soundOne = new SoundCard ("Creative Sound Blaster Audigy FX 5.1", 49.99);
        public static SoundCard soundTwo = new SoundCard ("Sound Blaster Z PCIe", 112.33);
        public static SoundCard soundThree = new SoundCard ("Creative Sound Blaster X AE-5", 193.91);

        public static OpSys opOne = new OpSys ("Microsoft Windows 10 Home", 105.00);
        public static OpSys opTwo = new OpSys ("Mac OS X Lion 10.7", 39.99);
        public static OpSys opThree = new OpSys ("Microsoft Windows 10 Pro",129.99);

        public static Display disOne = new Display("Samsung", 144.99, 22);
        public static Display disTwo = new Display ("LG Electronics", 169.99, 24);
        public static Display disThree = new Display ("Sceptre", 287.61, 27);

        public static Computer comp1 = new Computer(cpuTwo, ramTwo, hardOne, soundOne, opTwo, disOne);
        public static Computer comp2 = new Computer (cpuOne, ramOne, hardTwo, soundTwo, opOne, disTwo);
        public static Computer comp3 = new Computer (cpuThree, ramThree, hardThree, soundThree, opThree, disThree);

        public static Components[] prodList = {ramOne, ramTwo, ramThree,
                                               cpuOne, cpuTwo, cpuThree,
                                                hardOne, hardTwo, hardThree,
                                                soundOne, soundTwo, soundThree,
                                                opOne, opTwo, opThree,
                                                disOne, disTwo, disThree};
        public static Computer[] compList = { comp1, comp2, comp3};
    }
}