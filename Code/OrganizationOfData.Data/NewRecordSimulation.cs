namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;

    public class NewRecordSimulation
    {
        public BulkFile BulkFile { get; set; }

        public Person NewPerson { get; set; }

        public bool OverrunZone { get; set; }

        public int Column { get; set; }

        public string Message { get; set; }

        public bool IsFinished { get; set; }

        public bool Changed { get; set; }

        public int KeyTransformation { get; set; }

        public NewRecordSimulation(BulkFile bulkFile, Person newPerson)
        {
            BulkFile = bulkFile;
            NewPerson = newPerson;

            KeyTransformation = -1;
            OverrunZone = false;
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
                if (KeyTransformation == -1 && !OverrunZone)
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

                    Message += string.Format("Radimo transformaciju ključa metodom {0}. Dobijamo adresu baketa: {1}. Učitavamo taj baket. ", methodName, KeyTransformation);
                }
                else
                {
                    Column++;

                    if (Column >= BulkFile.Factor && !OverrunZone)
                    {
                        OverrunZone = true;
                        Column = -1;

                        Message += string.Format("Primarna zona je zauzeta. Prelazimo u zonu prekoračenja! ");
                    }
                    else if (Column >= BulkFile.NumberOfRecordsInOverrunZone && OverrunZone)
                    {
                        Message += "Došli smo do kraja zone prekoračenja. Nema mesta za dodavanje novog sloga u datoteku! ";
                        Column = -1;

                        IsFinished = true;
                        Message += string.Format("Kliknite sledeći korak radi završetka simulacije. ");
                    }
                    else
                    {
                        Record record = !OverrunZone ? BulkFile.PrimaryZone[KeyTransformation].Records[Column] : BulkFile.OverrunZone[Column];
                        if (!OverrunZone)
                            Message += string.Format("Proveravamo {0}. slog u primarnoj zoni. ", Column + 1);
                        else
                        {
                            if (record.Status == Status.empty)
                                Message += string.Format("Došli smo do slobodnog mesta u zoni prekoračenja. ", Column + 1);
                            else
                                Message += string.Format("Proveravamo {0}. slog u zoni prekoračenja. ", Column + 1);
                        }
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
                            if (!OverrunZone)
                            {
                                Message += string.Format("Trenutni slog je prazan. Upisujemo novi slog u trenutni slog i označavamo ga kao aktivan. ");
                                BulkFile.PrimaryZone[KeyTransformation].Records[Column].Person = NewPerson;
                                BulkFile.PrimaryZone[KeyTransformation].Records[Column].Status = Status.active;
                            }
                            else
                            {
                                Message += string.Format("Upisujemo novi slog u slobodno mesto i označavamo ga kao aktivan. ");
                                BulkFile.OverrunZone[Column].Person = NewPerson;
                                BulkFile.OverrunZone[Column].Status = Status.active;

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
