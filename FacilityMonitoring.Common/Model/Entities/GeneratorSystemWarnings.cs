using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.Model {


    public partial class GeneratorSystemError {
        public int Id { get; set; }

        public int H2GenReadingId { get; set; }
        public H2GenReading H2GenReading { get; set; }

        public WarningErrorKey this[SystemError error] {
            get {
                switch (error) {
                    case SystemError.E01_A1:
                        return this.E01_A1;
                    case SystemError.E01_A2:
                        return this.E01_A2;

                    case SystemError.E01_A3:
                        return this.E01_A3;

                    case SystemError.E01_B1:
                        return this.E01_B1;

                    case SystemError.E01_B2:
                        return this.E01_B2;

                    case SystemError.E01_B3:
                        return this.E01_B3;

                    case SystemError.E01_C1:
                        return this.E01_C1;

                    case SystemError.E01_C2:
                        return this.E01_A1;

                    case SystemError.E01_C3:
                        return this.E01_C3;

                    case SystemError.E02_A1:
                        return this.E02_A1;

                    case SystemError.E02_A2:
                        return this.E02_A2;

                    case SystemError.E02_A3:
                        return this.E02_A3;

                    case SystemError.E02_B1:
                        return this.E02_B1;

                    case SystemError.E02_B2:
                        return this.E02_B2;

                    case SystemError.E02_B3:
                        return this.E02_B3;

                    case SystemError.E02_C1:
                        return this.E02_C1;

                    case SystemError.E02_C2:
                        return this.E02_C2;

                    case SystemError.E02_C3:
                        return this.E02_C3;

                    case SystemError.E03_A:
                        return this.E03_A;

                    case SystemError.E03_B:
                        return this.E03_B;

                    case SystemError.E03_C:
                        return this.E03_C;

                    case SystemError.E04_A:
                        return this.E04_A;

                    case SystemError.E04_B:
                        return this.E04_B;

                    case SystemError.E04_C:
                        return this.E04_C;

                    case SystemError.E05_A1:
                        return this.E05_A1;

                    case SystemError.E05_A2:
                        return this.E05_A2;

                    case SystemError.E05_A3:
                        return this.E05_A3;

                    case SystemError.E05_B1:
                        return this.E05_B1;

                    case SystemError.E05_B2:
                        return this.E05_B2;

                    case SystemError.E05_B3:
                        return this.E05_B3;

                    case SystemError.E05_C1:
                        return this.E05_C1;

                    case SystemError.E05_C2:
                        return this.E05_C2;

                    case SystemError.E05_C3:
                        return this.E05_C3;

                    case SystemError.E06:
                        return this.E06;

                    case SystemError.E07:
                        return this.E07;

                    case SystemError.E08:
                        return this.E08;

                    case SystemError.E09:
                        return this.E09;

                    case SystemError.E10:
                        return this.E10;

                    case SystemError.E11:
                        return this.E11;

                    case SystemError.E12:
                        return this.E12;

                    case SystemError.E13:
                        return this.E13;

                    case SystemError.E14:
                        return this.E14;

                    case SystemError.E15:
                        return this.E15;

                    case SystemError.E16_A:
                        return this.E16_A;

                    case SystemError.E16_B:
                        return this.E16_B;

                    case SystemError.E17:
                        return this.E17;

                    case SystemError.E18:
                        return this.E18;

                    case SystemError.E19:
                        return this.E19;

                    case SystemError.E20_A:
                        return this.E20_A;

                    case SystemError.E20_B:
                        return this.E20_B;

                    case SystemError.E21:
                        return this.E21;

                    case SystemError.E22:
                        return this.E22;

                    case SystemError.E23:
                        return this.E23;

                    case SystemError.E24:
                        return this.E24;

                    case SystemError.E25:
                        return this.E25;

                    case SystemError.E26:
                        return this.E26;

                    case SystemError.E27:
                        return this.E27;

                    case SystemError.E28:
                        return this.E28;

                    case SystemError.E29:
                        return this.E29;

                    case SystemError.E30:
                        return this.E30;

                    case SystemError.E31:
                        return this.E31;

                    case SystemError.E32:
                        return this.E32;

                    case SystemError.E33:
                        return this.E33;

                    case SystemError.E34:
                        return this.E34;

                    case SystemError.E35:
                        return this.E35;

                    case SystemError.E36_A:
                        return this.E36_A;

                    case SystemError.E36_B:
                        return this.E36_B;

                    case SystemError.E36_C:
                        return this.E36_C;

                    case SystemError.E37:
                        return this.E37;

                    case SystemError.E38:
                        return this.E38;

                    case SystemError.E39:
                        return this.E39;

                    case SystemError.E40:
                        return this.E40;

                    case SystemError.E41:
                        return this.E41;

                    case SystemError.E42:
                        return this.E42;

                    case SystemError.E43:
                        return this.E43;

                    case SystemError.E44:
                        return this.E44;

                    case SystemError.E45:
                        return this.E45;

                    case SystemError.E46:
                        return this.E46;

                    case SystemError.E47:
                        return this.E47;

                    case SystemError.E48:
                        return this.E48;
                    default:
                        return 0;
                }
            }
            set {
                switch (error) {
                    case SystemError.E01_A1:
                        this.E01_A1 = value;
                        break;
                    case SystemError.E01_A2:
                        this.E01_A2 = value;
                        break;
                    case SystemError.E01_A3:
                        this.E01_A3 = value;
                        break;
                    case SystemError.E01_B1:
                        this.E01_B1 = value;
                        break;
                    case SystemError.E01_B2:
                        this.E01_B2 = value;
                        break;
                    case SystemError.E01_B3:
                        this.E01_B3 = value;
                        break;
                    case SystemError.E01_C1:
                        this.E01_C1 = value;
                        break;
                    case SystemError.E01_C2:
                        this.E01_A1 = value;
                        break;
                    case SystemError.E01_C3:
                        this.E01_C3 = value;
                        break;
                    case SystemError.E02_A1:
                        this.E02_A1 = value;
                        break;
                    case SystemError.E02_A2:
                        this.E02_A2 = value;
                        break;
                    case SystemError.E02_A3:
                        this.E02_A3 = value;
                        break;
                    case SystemError.E02_B1:
                        this.E02_B1 = value;
                        break;
                    case SystemError.E02_B2:
                        this.E02_B2 = value;
                        break;
                    case SystemError.E02_B3:
                        this.E02_B3 = value;
                        break;
                    case SystemError.E02_C1:
                        this.E02_C1 = value;
                        break;
                    case SystemError.E02_C2:
                        this.E02_C2 = value;
                        break;
                    case SystemError.E02_C3:
                        this.E02_C3 = value;
                        break;
                    case SystemError.E03_A:
                        this.E03_A = value;
                        break;
                    case SystemError.E03_B:
                        this.E03_B = value;
                        break;
                    case SystemError.E03_C:
                        this.E03_C = value;
                        break;
                    case SystemError.E04_A:
                        this.E04_A = value;
                        break;
                    case SystemError.E04_B:
                        this.E04_B = value;
                        break;
                    case SystemError.E04_C:
                        this.E04_C = value;
                        break;
                    case SystemError.E05_A1:
                        this.E05_A1 = value;
                        break;
                    case SystemError.E05_A2:
                        this.E05_A2 = value;
                        break;
                    case SystemError.E05_A3:
                        this.E05_A3 = value;
                        break;
                    case SystemError.E05_B1:
                        this.E05_B1 = value;
                        break;
                    case SystemError.E05_B2:
                        this.E05_B2 = value;
                        break;
                    case SystemError.E05_B3:
                        this.E05_B3 = value;
                        break;
                    case SystemError.E05_C1:
                        this.E05_C1 = value;
                        break;
                    case SystemError.E05_C2:
                        this.E05_C2 = value;
                        break;
                    case SystemError.E05_C3:
                        this.E05_C3 = value;
                        break;
                    case SystemError.E06:
                        this.E06 = value;
                        break;
                    case SystemError.E07:
                        this.E07 = value;
                        break;
                    case SystemError.E08:
                        this.E08 = value;
                        break;
                    case SystemError.E09:
                        this.E09 = value;
                        break;
                    case SystemError.E10:
                        this.E10 = value;
                        break;
                    case SystemError.E11:
                        this.E11 = value;
                        break;
                    case SystemError.E12:
                        this.E12 = value;
                        break;
                    case SystemError.E13:
                        this.E13 = value;
                        break;
                    case SystemError.E14:
                        this.E14 = value;
                        break;
                    case SystemError.E15:
                        this.E15 = value;
                        break;
                    case SystemError.E16_A:
                        this.E16_A = value;
                        break;
                    case SystemError.E16_B:
                        this.E16_B = value;
                        break;
                    case SystemError.E17:
                        this.E17 = value;
                        break;
                    case SystemError.E18:
                        this.E18 = value;
                        break;
                    case SystemError.E19:
                        this.E19 = value;
                        break;
                    case SystemError.E20_A:
                        this.E20_A = value;
                        break;
                    case SystemError.E20_B:
                        this.E20_B = value;
                        break;
                    case SystemError.E21:
                        this.E21 = value;
                        break;
                    case SystemError.E22:
                        this.E22 = value;
                        break;
                    case SystemError.E23:
                        this.E23 = value;
                        break;
                    case SystemError.E24:
                        this.E24 = value;
                        break;
                    case SystemError.E25:
                        this.E25 = value;
                        break;
                    case SystemError.E26:
                        this.E26 = value;
                        break;
                    case SystemError.E27:
                        this.E27 = value;
                        break;
                    case SystemError.E28:
                        this.E28 = value;
                        break;
                    case SystemError.E29:
                        this.E29 = value;
                        break;
                    case SystemError.E30:
                        this.E30 = value;
                        break;
                    case SystemError.E31:
                        this.E31 = value;
                        break;
                    case SystemError.E32:
                        this.E32 = value;
                        break;
                    case SystemError.E33:
                        this.E33 = value;
                        break;
                    case SystemError.E34:
                        this.E34 = value;
                        break;
                    case SystemError.E35:
                        this.E35 = value;
                        break;
                    case SystemError.E36_A:
                        this.E36_A = value;
                        break;
                    case SystemError.E36_B:
                        this.E36_B = value;
                        break;
                    case SystemError.E36_C:
                        this.E36_C = value;
                        break;
                    case SystemError.E37:
                        this.E37 = value;
                        break;
                    case SystemError.E38:
                        this.E38 = value;
                        break;
                    case SystemError.E39:
                        this.E39 = value;
                        break;
                    case SystemError.E40:
                        this.E40 = value;
                        break;
                    case SystemError.E41:
                        this.E41 = value;
                        break;
                    case SystemError.E42:
                        this.E42 = value;
                        break;
                    case SystemError.E43:
                        this.E43 = value;
                        break;
                    case SystemError.E44:
                        this.E44 = value;
                        break;
                    case SystemError.E45:
                        this.E45 = value;
                        break;
                    case SystemError.E46:
                        this.E46 = value;
                        break;
                    case SystemError.E47:
                        this.E47 = value;
                        break;
                    case SystemError.E48:
                        this.E48 = value;
                        break;
                }
            }
        }

        public WarningErrorKey E01_A1 { get; set; }
        public WarningErrorKey E01_A2 { get; set; }
        public WarningErrorKey E01_A3 { get; set; }
        public WarningErrorKey E01_B1 { get; set; }
        public WarningErrorKey E01_B2 { get; set; }
        public WarningErrorKey E01_B3 { get; set; }
        public WarningErrorKey E01_C1 { get; set; }
        public WarningErrorKey E01_C2 { get; set; }
        public WarningErrorKey E01_C3 { get; set; }
        public WarningErrorKey E02_A1 { get; set; }
        public WarningErrorKey E02_A2 { get; set; }
        public WarningErrorKey E02_A3 { get; set; }
        public WarningErrorKey E02_B1 { get; set; }
        public WarningErrorKey E02_B2 { get; set; }
        public WarningErrorKey E02_B3 { get; set; }
        public WarningErrorKey E02_C1 { get; set; }
        public WarningErrorKey E02_C2 { get; set; }
        public WarningErrorKey E02_C3 { get; set; }
        public WarningErrorKey E03_A { get; set; }
        public WarningErrorKey E03_B { get; set; }
        public WarningErrorKey E03_C { get; set; }
        public WarningErrorKey E04_A { get; set; }
        public WarningErrorKey E04_B { get; set; }
        public WarningErrorKey E04_C { get; set; }
        public WarningErrorKey E05_A1 { get; set; }
        public WarningErrorKey E05_A2 { get; set; }
        public WarningErrorKey E05_A3 { get; set; }
        public WarningErrorKey E05_B1 { get; set; }
        public WarningErrorKey E05_B2 { get; set; }
        public WarningErrorKey E05_B3 { get; set; }
        public WarningErrorKey E05_C1 { get; set; }
        public WarningErrorKey E05_C2 { get; set; }
        public WarningErrorKey E05_C3 { get; set; }
        public WarningErrorKey E06 { get; set; }
        public WarningErrorKey E07 { get; set; }
        public WarningErrorKey E08 { get; set; }
        public WarningErrorKey E09 { get; set; }
        public WarningErrorKey E10 { get; set; }
        public WarningErrorKey E11 { get; set; }
        public WarningErrorKey E12 { get; set; }
        public WarningErrorKey E13 { get; set; }
        public WarningErrorKey E14 { get; set; }
        public WarningErrorKey E15 { get; set; }
        public WarningErrorKey E16_A { get; set; }
        public WarningErrorKey E16_B { get; set; }
        public WarningErrorKey E17 { get; set; }
        public WarningErrorKey E18 { get; set; }
        public WarningErrorKey E19 { get; set; }
        public WarningErrorKey E20_A { get; set; }
        public WarningErrorKey E20_B { get; set; }
        public WarningErrorKey E21 { get; set; }
        public WarningErrorKey E22 { get; set; }
        public WarningErrorKey E23 { get; set; }
        public WarningErrorKey E24 { get; set; }
        public WarningErrorKey E25 { get; set; }
        public WarningErrorKey E26 { get; set; }
        public WarningErrorKey E27 { get; set; }
        public WarningErrorKey E28 { get; set; }
        public WarningErrorKey E29 { get; set; }
        public WarningErrorKey E30 { get; set; }
        public WarningErrorKey E31 { get; set; }
        public WarningErrorKey E32 { get; set; }
        public WarningErrorKey E33 { get; set; }
        public WarningErrorKey E34 { get; set; }
        public WarningErrorKey E35 { get; set; }
        public WarningErrorKey E36_A { get; set; }
        public WarningErrorKey E36_B { get; set; }
        public WarningErrorKey E36_C { get; set; }
        public WarningErrorKey E37 { get; set; }
        public WarningErrorKey E38 { get; set; }
        public WarningErrorKey E39 { get; set; }
        public WarningErrorKey E40 { get; set; }
        public WarningErrorKey E41 { get; set; }
        public WarningErrorKey E42 { get; set; }
        public WarningErrorKey E43 { get; set; }
        public WarningErrorKey E44 { get; set; }
        public WarningErrorKey E45 { get; set; }
        public WarningErrorKey E46 { get; set; }
        public WarningErrorKey E47 { get; set; }
        public WarningErrorKey E48 { get; set; }
    }

    public partial class GeneratorSystemWarning {
        public int Id { get; set; }

        public int H2GenReadingId { get; set; }
        public H2GenReading H2GenReading { get; set; }

        public WarningErrorKey this[SystemWarning warning] {
            set {
                switch (warning) {
                    case SystemWarning.W01:
                        this.W01 = value;
                        break;
                    case SystemWarning.W02:
                        this.W02 = value;
                        break;
                    case SystemWarning.W03:
                        this.W03 = value;
                        break;
                    case SystemWarning.W04:
                        this.W04 = value;
                        break;
                    case SystemWarning.W05:
                        this.W05 = value;
                        break;
                    case SystemWarning.W06:
                        this.W06 = value;
                        break;
                    case SystemWarning.W07:
                        this.W07 = value;
                        break;
                    case SystemWarning.W08:
                        this.W08 = value;
                        break;
                    case SystemWarning.W09:
                        this.W09 = value;
                        break;
                    case SystemWarning.W10:
                        this.W10 = value;
                        break;
                    case SystemWarning.W11:
                        this.W11 = value;
                        break;
                    case SystemWarning.W12:
                        this.W12 = value;
                        break;
                    case SystemWarning.W13:
                        this.W13 = value;
                        break;
                    case SystemWarning.W14:
                        this.W14 = value;
                        break;
                    case SystemWarning.W15:
                        this.W15 = value;
                        break;
                    case SystemWarning.W16:
                        this.W16 = value;
                        break;
                    case SystemWarning.W17:
                        this.W17 = value;
                        break;
                    case SystemWarning.W18:
                        this.W18 = value;
                        break;
                    case SystemWarning.W19:
                        this.W19 = value;
                        break;
                    case SystemWarning.W20:
                        this.W20 = value;
                        break;
                    case SystemWarning.W21:
                        this.W21 = value;
                        break;
                    case SystemWarning.W22:
                        this.W22 = value;
                        break;
                }
            }
            get {
                switch (warning) {
                    case SystemWarning.W01:
                        return this.W01;
                    case SystemWarning.W02:
                        return this.W02;
                    case SystemWarning.W03:
                        return this.W03;
                    case SystemWarning.W04:
                        return this.W04;
                    case SystemWarning.W05:
                        return this.W05;
                    case SystemWarning.W06:
                        return this.W06;
                    case SystemWarning.W07:
                       return this.W07;
                    case SystemWarning.W08:
                        return this.W08;
                    case SystemWarning.W09:
                        return this.W09;
                    case SystemWarning.W10:
                        return this.W10;
                    case SystemWarning.W11:
                        return this.W11;
                    case SystemWarning.W12:
                        return this.W12;
                    case SystemWarning.W13:
                        return this.W13;
                    case SystemWarning.W14:
                        return this.W14;
                    case SystemWarning.W15:
                        return this.W15;
                    case SystemWarning.W16:
                        return this.W16;
                    case SystemWarning.W17:
                        return this.W17;
                    case SystemWarning.W18:
                        return this.W18;
                    case SystemWarning.W19:
                        return this.W19;
                    case SystemWarning.W20:
                        return this.W20;
                    case SystemWarning.W21:
                        return this.W21;
                    case SystemWarning.W22:
                        return this.W22;
                    default:
                        return 0;
                }
            }
        }

        public WarningErrorKey W01 { get; set; }
        public WarningErrorKey W02 { get; set; }
        public WarningErrorKey W03 { get; set; }
        public WarningErrorKey W04 { get; set; }
        public WarningErrorKey W05 { get; set; }
        public WarningErrorKey W06 { get; set; }
        public WarningErrorKey W07 { get; set; }
        public WarningErrorKey W08 { get; set; }
        public WarningErrorKey W09 { get; set; }
        public WarningErrorKey W10 { get; set; }
        public WarningErrorKey W11 { get; set; }
        public WarningErrorKey W12 { get; set; }
        public WarningErrorKey W13 { get; set; }
        public WarningErrorKey W14 { get; set; }
        public WarningErrorKey W15 { get; set; }
        public WarningErrorKey W16 { get; set; }
        public WarningErrorKey W17 { get; set; }
        public WarningErrorKey W18 { get; set; }
        public WarningErrorKey W19 { get; set; }
        public WarningErrorKey W20 { get; set; }
        public WarningErrorKey W21 { get; set; }
        public WarningErrorKey W22 { get; set; }

    }
}
