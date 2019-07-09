namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;

    public class DeleteRecordSimulation
    {
        public BulkFile BulkFile { get; set; }

        public int Id { get; set; }

        public bool Logical { get; set; }

        public bool OverrunZone { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public string Message { get; set; }

        public bool IsFinished { get; set; }

        public bool Changed { get; set; }

        public int KeyTransformation { get; set; }

        public bool DeletedInPrimary { get; set; }

        public bool DeletedInOverrun { get; set; }

        public DeleteRecordSimulation(BulkFile bulkFile, int id, bool logical)
        {
            BulkFile = bulkFile;
            Id = id;
            Logical = logical;
            KeyTransformation = -1;

            OverrunZone = false;
            Row = -1;
            Column = -1;
        }

        public bool NextStep()
        {
            Message = "";
            Changed = false;

            if (IsFinished)
            {
                return false;
            }
            else
            {
                if (Deleted)
                {
                    if (DeletedInPrimary)
                    {
                        if (!OverrunZone)
                        {
                            Column++;

                            if (Column >= BulkFile.Factor)
                            {
                                Message += "Došli smo do kraja trenutnog baketa. ";

                                if (BulkFile.PrimaryZone[KeyTransformation].OverrunedRecords == 0)
                                {
                                    BulkFile.PrimaryZone[Row].Records[BulkFile.Factor - 1].Person = new Person();
                                    BulkFile.PrimaryZone[Row].Records[BulkFile.Factor - 1].Status = Status.empty;
                                    Changed = true;

                                    Message += "Nemamo slogove u zoni prekoračenja koji pripadaju trenutnom baketu. Brišemo poslednji slog i završavamo sa šiftovanjem. ";
                                    Column = BulkFile.Factor - 1;
                                    IsFinished = true;
                                    Message += string.Format("Kliknite sledeći korak radi završetka simulacije. ");
                                }
                                else
                                {
                                    Column = -1;
                                    Row = 0;
                                    Message += string.Format("Prelazimo na baket sa adresom: {0}. ", BulkFile.OverrunZone[Row].Address);
                                    OverrunZone = true;
                                }
                            }
                            else
                            {
                                BulkFile.PrimaryZone[Row].Records[Column - 1].Person = BulkFile.PrimaryZone[Row].Records[Column].Person;
                                BulkFile.PrimaryZone[Row].Records[Column - 1].Status = BulkFile.PrimaryZone[Row].Records[Column].Status;

                                Changed = true;
                                Message += string.Format("Prebacujemo {0}. slog u {1}. slog u trenutnom baketu. ", Column + 1, Column);

                                if (!OverrunZone ? BulkFile.PrimaryZone[Row].Records[Column].Status == Status.empty : BulkFile.OverrunZone[Row].Records[Column].Status == Status.empty)
                                {
                                    Message += string.Format("Došli smo do praznog sloga. Šiftovanje je završeno! Kliknite sledeći korak radi završetka simulacije. ");
                                    IsFinished = true;
                                }
                            }
                        }
                        else
                        {
                            Column++;

                            if (Column >= BulkFile.Factor)
                            {
                                Message = "Došli smo do kraja trenutnog baketa. ";
                                Row++;

                                if (Row >= BulkFile.NumberOfBuckets)
                                {
                                    throw new System.Exception();
                                }
                                else
                                {
                                    Message += string.Format("Prelazimo na baket sa adresom: {0}. ", BulkFile.OverrunZone[Row].Address);
                                    Column = -1;
                                }
                            }
                            else
                            {
                                Message += string.Format("Poveramo {0}. slog u trenutnom baketu. ", Column + 1);
                                if (KeyTransformations.ResidualSplitting(BulkFile.OverrunZone[Row].Records[Column].Person.Id, BulkFile.NumberOfBuckets) == KeyTransformation)
                                {
                                    Message += string.Format("Našli smo prvi slog koji pripada traženom baketu. Prebacujemo ga u primarnu zonu i šiftujemo ostale slogove u zoni prekoračenja za 1 mesto ulevo. ");

                                    BulkFile.PrimaryZone[KeyTransformation].OverrunedRecords--;
                                    DeletedInOverrun = true;
                                    DeletedInPrimary = false;

                                    BulkFile.PrimaryZone[KeyTransformation].Records[BulkFile.Factor - 1].Person = BulkFile.OverrunZone[Row].Records[Column].Person;
                                    BulkFile.PrimaryZone[KeyTransformation].Records[BulkFile.Factor - 1].Status = BulkFile.OverrunZone[Row].Records[Column].Status;
                                    Changed = true;
                                }
                                else
                                {
                                    Message += string.Format("Id trenutnog sloga ne pripada traženom baketu. ");
                                }

                                if (BulkFile.OverrunZone[Row].Records[Column].Status == Status.empty)
                                {
                                    throw new System.Exception();
                                }
                            }
                        }
                    }
                    else if (DeletedInOverrun)
                    {
                        Column++;

                        if (Column >= BulkFile.Factor)
                        {
                            Message = "Došli smo do kraja trenutnog baketa. ";
                            Row++;

                            if (Row >= BulkFile.NumberOfBuckets)
                            {
                                Message = "Došli smo do kraja zone prekoračenja. Završili smo sa šiftovanjem ulevo. ";
                                Row = -1;

                                IsFinished = true;
                                Message += string.Format("Kliknite sledeći korak radi završetka simulacije. ");
                            }
                            else
                            {
                                Message += string.Format("Prelazimo na baket sa adresom: {0}. ", BulkFile.OverrunZone[Row].Address);
                                Column = -1;
                            }
                        }
                        else
                        {
                            if (Column == 0)
                            {
                                BulkFile.OverrunZone[Row - 1].Records[BulkFile.Factor - 1].Person = BulkFile.OverrunZone[Row].Records[Column].Person;
                                BulkFile.OverrunZone[Row - 1].Records[BulkFile.Factor - 1].Status = BulkFile.OverrunZone[Row].Records[Column].Status;

                                Message += string.Format("Prebacujemo prvi slog iz baketa sa adresom {0} u poslednji slog baketa sa adresom {1}. ", BulkFile.OverrunZone[Row - 1].Address, BulkFile.OverrunZone[Row].Address);
                                Changed = true;
                            }
                            else
                            {
                                BulkFile.OverrunZone[Row].Records[Column - 1].Person = BulkFile.OverrunZone[Row].Records[Column].Person;
                                BulkFile.OverrunZone[Row].Records[Column - 1].Status = BulkFile.OverrunZone[Row].Records[Column].Status;

                                Changed = true;
                                Message += string.Format("Prebacujemo {0}. slog u {1}. slog u trenutnom baketu. ", Column + 1, Column);
                            }

                            if (!OverrunZone ? BulkFile.PrimaryZone[Row].Records[Column].Status == Status.empty : BulkFile.OverrunZone[Row].Records[Column].Status == Status.empty)
                            {
                                Message += string.Format("Došli smo do praznog sloga. Šiftovanje je završeno! Kliknite sledeći korak radi završetka simulacije. ");
                                IsFinished = true;
                            }
                        }
                    }
                }
                else
                {
                    if (Row == -1)
                    {
                        if (!OverrunZone)
                        {
                            string methodName = null;

                            switch (BulkFile.TransformationMethod)
                            {
                                case TransformationMethod.centralKeyDigits:
                                    KeyTransformation = KeyTransformations.CentralKeyDigits(Id, BulkFile.NumberOfBuckets);
                                    methodName = "centralnih kvadrata ključeva";
                                    break;
                                case TransformationMethod.overlap:
                                    KeyTransformation = KeyTransformations.Overlap(Id, BulkFile.NumberOfBuckets);
                                    methodName = "metodom preklapanja";
                                    break;
                                case TransformationMethod.residualSplitting:
                                    KeyTransformation = KeyTransformations.ResidualSplitting(Id, BulkFile.NumberOfBuckets);
                                    methodName = "ostataka pri deljenju";
                                    break;
                            }
                            Row = KeyTransformation;

                            Message = string.Format("Radimo transformaciju ključa metodom {0}. Dobijamo adresu baketa: {1}. Učitavamo taj baket. ", methodName, Row);
                        }
                        else
                        {
                            Row = 0;
                            Message = string.Format("Učitavamo baket sa adresom {0}. ", BulkFile.OverrunZone[Row].Address);
                        }
                    }
                    else
                    {
                        Column++;

                        if (Column >= BulkFile.Factor)
                        {
                            if (!OverrunZone)
                            {
                                OverrunZone = true;
                                Row = -1;
                                Column = -1;

                                Message += string.Format("Slog nije pronađen u primarnoj zoni. Prelazimo u zonu prekoračenja.");
                            }
                            else
                            {
                                Message = "Došli smo do kraja trenutnog baketa. ";
                                Row++;

                                if (Row >= BulkFile.NumberOfBuckets)
                                {
                                    Message += "Došli smo do kraja zone prekoračenja. Ne postoji slog sa unetim id-jem u datoteci. ";
                                    Row = -1;

                                    IsFinished = true;
                                    Message += string.Format("Kliknite sledeći korak radi završetka simulacije. ");
                                }
                                else
                                {
                                    Message += string.Format("Prelazimo na baket sa adresom: {0}. ", BulkFile.OverrunZone[Row].Address);
                                    Column = -1;
                                }
                            }
                        }
                        else
                        {
                            Record record = !OverrunZone ? BulkFile.PrimaryZone[Row].Records[Column] : BulkFile.OverrunZone[Row].Records[Column];
                            Message = string.Format("Proveravamo {0}. slog u baketu.", Column + 1);

                            if (record.Status != Status.empty && Id == record.Person.Id)
                            {
                                Message += string.Format("Našli smo slog koji treba obrisati. ");

                                if (Logical)
                                {
                                    if (record.Status == Status.inactive)
                                    {
                                        Message += string.Format("Traženi slog je već logički obrisan! Kliknite sledeći korak radi završetka simulacije. ");
                                    }
                                    else
                                    {
                                        if (!OverrunZone)
                                        {
                                            BulkFile.PrimaryZone[Row].Records[Column].Status = Status.inactive;
                                        }
                                        else
                                        {
                                            BulkFile.OverrunZone[Row].Records[Column].Status = Status.inactive;
                                        }
                                        Changed = true;

                                        Message += string.Format("Logički brišemo trenutni slog! Kliknite sledeći korak radi završetka simulacije. ");
                                    }

                                    IsFinished = true;
                                }
                                else
                                {
                                    if (!OverrunZone)
                                    {
                                        BulkFile.PrimaryZone[Row].Records[Column].Person = new Person();
                                        BulkFile.PrimaryZone[Row].Records[Column].Status = Status.empty;
                                        DeletedInPrimary = true;

                                        Message += string.Format("Fizički brišemo trenutni slog! Sada šiftujemo sve slogove iza trenutnog sloga za jedno mesto ulevo u trenutnom baketu. ");
                                    }
                                    else
                                    {
                                        BulkFile.OverrunZone[Row].Records[Column].Person = new Person();
                                        BulkFile.OverrunZone[Row].Records[Column].Status = Status.empty;
                                        BulkFile.PrimaryZone[KeyTransformation].OverrunedRecords--;
                                        DeletedInOverrun = true;

                                        Message += string.Format("Fizički brišemo trenutni slog! Sada šiftujemo sve slogove iza trenutnog sloga za jedno mesto ulevo. ");
                                    }
                                    Changed = true;
                                }
                            }
                            else if (record.Status == Status.empty)
                            {
                                Message += string.Format("Trenutni slog je prazan. Ne postoji slog sa unetim id-jem u datoteci.  ");
                                Message += string.Format("Kliknite sledeći korak radi završetka simulacije. ");

                                IsFinished = true;
                            }
                            else
                            {
                                Message += string.Format("Id od trenugnog sloga se ne poklapa sa id-jem sloga koji se traži. Prelazimo na sledeći slog. ");
                            }
                        }
                    }
                }

                return true;
            }
        }

        public bool Deleted
        {
            get
            {
                return DeletedInOverrun || DeletedInPrimary;
            }
        }
    }
}
