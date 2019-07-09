namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;

    public class EditRecordSimulation
    {
        public BulkFile BulkFile { get; set; }

        public int Id { get; set; }

        public string NewFullName { get; set; }

        public string NewAddress { get; set; }

        public int? NewAge { get; set; }

        public bool OverrunZone { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public string Message { get; set; }

        public bool IsFinished { get; set; }

        public EditRecordSimulation(BulkFile bulkFile, int id, string newFullName, string newAddress, int? newAge)
        {
            BulkFile = bulkFile;

            Id = id;
            NewFullName = newFullName;
            NewAddress = newAddress;
            NewAge = newAge;

            OverrunZone = false;
            Row = -1;
            Column = -1;
        }

        public bool NextStep()
        {
            Message = null;

            if (IsFinished)
            {
                return false;
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
                                Row = KeyTransformations.CentralKeyDigits(Id, BulkFile.NumberOfBuckets);
                                methodName = "centralnih kvadrata ključeva";
                                break;
                            case TransformationMethod.overlap:
                                Row = KeyTransformations.Overlap(Id, BulkFile.NumberOfBuckets);
                                methodName = "metodom preklapanja";
                                break;
                            case TransformationMethod.residualSplitting:
                                Row = KeyTransformations.ResidualSplitting(Id, BulkFile.NumberOfBuckets);
                                methodName = "ostataka pri deljenju";
                                break;
                        }

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

                            Message = string.Format("Zona prekoračenja je zauzeta. Prelazimo u zonu prekoračenja! ");
                        }
                        else
                        {
                            Message = "Došli smo do kraja trenutnog baketa. ";
                            Row++;

                            if (Row >= this.BulkFile.NumberOfBuckets)
                            {
                                Message = "Došli smo do kraja zone prekoračenja. Nema mesta za dodavanje novog sloga u datoteku! ";
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
                        Message = string.Format("Proveravamo {0}. slog u baketu. ", Column + 1);
                        if (record.Status != Status.empty && Id == record.Person.Id)
                        {
                            Message += string.Format("Našli smo slog koji treba izmeniti. Izmenjujemo slog. ");

                            if (NewFullName != null)
                            {
                                if (!OverrunZone)
                                    BulkFile.PrimaryZone[Row].Records[Column].Person.FullName = NewFullName;
                                else
                                    BulkFile.OverrunZone[Row].Records[Column].Person.FullName = NewFullName;
                            }
                            if (NewAddress != null)
                            {
                                if (!OverrunZone)
                                    BulkFile.PrimaryZone[Row].Records[Column].Person.Adress = NewAddress;
                                else
                                    BulkFile.OverrunZone[Row].Records[Column].Person.Adress = NewAddress;
                            }
                            if (NewAge != null)
                            {
                                if (!OverrunZone)
                                    BulkFile.PrimaryZone[Row].Records[Column].Person.Age = NewAge;
                                else
                                    BulkFile.OverrunZone[Row].Records[Column].Person.Age = NewAge;
                            }
                            IsFinished = true;
                            Message += string.Format("Kliknite sledeći korak radi završetka simulacije. ");
                        }
                        else if (record.Status != Status.empty)
                        {
                            Message += string.Format("Id od trenugnog sloga se ne poklapa sa id-jem sloga koji se traži. Prelazimo na sledeći slog. ");
                        }
                        else
                        {
                            Message += string.Format("Trenutni slog je prazan. Ne postoji slog sa unetim id-jem u datoteci. ");

                            IsFinished = true;
                            Message += string.Format("Kliknite sledeći korak radi završetka simulacije. ");
                        }
                    }
                }
                return true;
            }
        }
    }
}
