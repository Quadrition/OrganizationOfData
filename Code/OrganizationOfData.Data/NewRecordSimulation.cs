namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;

    public class NewRecordSimulation
    {
        public BulkFile BulkFile { get; set; }

        public Person NewPerson { get; set; }

        public bool OverrunZone { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public string Message { get; set; }

        public bool IsFinished { get; set; }

        public bool Changed { get; set; }

        public int KeyTransformation { get; set; }

        public NewRecordSimulation(BulkFile bulkFile, Person newPerson)
        {
            BulkFile = bulkFile;
            NewPerson = newPerson;

            OverrunZone = false;
            Row = -1;
            Column = -1;
        }

        public bool NextStep()
        {
            Message = null;
            Changed = false;

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
                                KeyTransformation = KeyTransformations.CentralKeyDigits(NewPerson.Id, BulkFile.NumberOfBuckets);
                                methodName = "centralnih kvadrata ključeva";
                                break;
                            case TransformationMethod.overlap:
                                KeyTransformation = KeyTransformations.Overlap(NewPerson.Id, BulkFile.NumberOfBuckets);
                                methodName = "metodom preklapanja";
                                break;
                            case TransformationMethod.residualSplitting:
                                KeyTransformation = KeyTransformations.ResidualSplitting(NewPerson.Id, BulkFile.NumberOfBuckets);
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

                    if (Column >= this.BulkFile.Factor)
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
                        Message = string.Format("Proveravamo {0}. slog u baketu.", Column + 1);
                        if (record.Status != Status.empty && NewPerson.Id == record.Person.Id)
                        {
                            Message += string.Format("Trenutni slog ima isti id kao novi slog! Unos sloga prekinut! ");

                            IsFinished = true;
                            Message += string.Format("Kliknite sledeći korak radi završetka simulacije. ");
                        }
                        else if (record.Status != Status.empty)
                        {
                            Message += string.Format("Trenutni slog je zauzet. Prelazimo na sledeći slog. ");
                        }
                        else
                        {
                            Message += string.Format("Trenutni slog je prazan. Upisujemo novi slog u trenutni slog i označavamo ga kao aktivan. ");

                            if (!OverrunZone)
                            {
                                BulkFile.PrimaryZone[Row].Records[Column].Person = NewPerson;
                                BulkFile.PrimaryZone[Row].Records[Column].Status = Status.active;
                            }
                            else
                            {
                                BulkFile.OverrunZone[Row].Records[Column].Person = NewPerson;
                                BulkFile.OverrunZone[Row].Records[Column].Status = Status.active;

                                BulkFile.PrimaryZone[KeyTransformation].OverrunedRecords++;
                            }
                            Changed = true;

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
