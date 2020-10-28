using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMA3A.part3.computerParts
{
    public class Computer
    {
        private Components cpu;
        private Components ram;
        private Components hardDrive;
        private Components soundCard;
        private Components opSys;
        private Components display;

        public Computer(Components CPU, Components Ram, Components HardDrive, Components SoundCard, Components OpSys, Components Display)
        {
            this.cpu = CPU;
            this.ram = Ram;
            this.hardDrive = HardDrive;
            this.soundCard = SoundCard;
            this.opSys = OpSys;
            this.display = Display;
        }

        public Components CPU
        {
            get { return cpu; }
            set { cpu = value; }
        }

        public Components Ram
        {
            get { return ram; }
            set { ram = value; }
        }

        public Components HardDrive
        {
            get { return hardDrive; }
            set { hardDrive = value; }
        }

        public Components SoundDrive
        {
            get { return soundCard; }
            set { soundCard = value; }
        }

        public Components OpSys
        {
            get { return opSys; }
            set { opSys = value; }
        }

        public Components Display
        {
            get { return display; }
            set { display = value; }
        }

        public double TotalPrice ()
        {
            var total = 0.0;
            total += cpu.Price;
            total += ram.Price;
            total += hardDrive.Price;
            total += soundCard.Price;
            total += opSys.Price;
            total += display.Price;

            return total;
        }
        public override string ToString()
        {
            return cpu.ToString() + ", " + ram.ToString() + ", " + hardDrive.ToString() + ", " + soundCard.ToString()
                + ", " + display.ToString() + ", " + opSys.ToString();
        }

    }
}