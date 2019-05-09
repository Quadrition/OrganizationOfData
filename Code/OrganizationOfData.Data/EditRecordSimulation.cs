namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;

    public class EditRecordSimulation
    {
        private readonly BulkFileWithSerialOverrunZone bulkFile;

        public Record EditedRecord { get; set; }

        public bool OverrunZone { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public string Message { get; set; }

        public bool IsFinished { get; set; }

        public EditRecordSimulation(BulkFileWithSerialOverrunZone bulkFile, Record editedRecord)
        {
            this.bulkFile = bulkFile;
            EditedRecord = editedRecord;

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

            if (OverrunZone == false)
            {
                if (Row == -1)
                {
                    int index = KeyTransformations.ResidualSplitting(EditedRecord.Person.Id, bulkFile.NumberOfBuckets);

                    string methodName = null;

                    switch (bulkFile.TransformationMethod)
                    {
                        case TransformationMethod.centralKeyDigits:
                            methodName = "centralnih kvadrata ključeva";
                            break;
                        case TransformationMethod.overlap:
                            methodName = "metodom preklapanja";
                            break;
                        case TransformationMethod.residualSplitting:
                            methodName = "ostataka pri deljenju";
                            break;
                    }

                    Message = string.Format("Radimo transformaciju ključa metodom {0}. Dobijamo adresu baketa: {1}. Učitavamo taj baket. ", methodName, index);
                    Row = index;

                    return true;
                }

                Column++;

                if (Column >= this.bulkFile.Factor)
                {
                    OverrunZone = true;
                    Row = -1;
                    Column = -1;

                    Message = string.Format("Slog nije pronađen u primarnoj zoni. Prelazimo u zonu prekoračenja. ");

                    return true;
                }

                Record record = bulkFile.PrimaryZone[Row].Records[Column];
                Message = string.Format("Proveravamo {0}. slog u baketu. " +
                    "", Column + 1);
                if (record.Status != Status.empty && EditedRecord.Person.Id == record.Person.Id)
                {
                    Message += string.Format("Našli smo slog koji treba izmeniti. ");

                    if (record.Status == Status.inactive)
                    {
                       
                        Column = -1;
                        Row = -1;

                        IsFinished = true;
                        Message += string.Format("Kliknite sledeći korak radi završetka simulacije. ");
                        return true;
                    }

                    
                }
                else if (record.Status != Status.empty)
                {
                    Message += string.Format("Trenutni slog je zauzet. Prelazimo na sledeći slog. ");
                    return true;
                }
                else if (record.Status == Status.empty)
                {
                    Message += string.Format("Trenutni slog je prazan. Upisujemo novi slog u trenutni slog i označavamo ga kao aktivan. ");

                    IsFinished = true;
                    Message += string.Format("Kliknite sledeći korak za upisivanje izmena u disk i završetka simulacije. ");
                    return true;
                }
            }
            else
            {
                if (Row == -1)
                {
                    Row = 0;
                    Message = string.Format("Učitavamo baket sa adresom {0}. ", bulkFile.OverrunZone[Row].Address);
                }

                Column++;

                if (Column >= this.bulkFile.Factor)
                {
                    Message = "Došli smo do kraja trenutnog baketa. ";
                    Row++;

                    if (Row >= this.bulkFile.NumberOfBuckets)
                    {
                        Message = "Došli smo do kraja zone prekoračenja. Nema mesta u datoteci za dodavanje novog sloga sa unetim id! ";
                        Column = -1;
                        Row = -1;

                        IsFinished = true;
                        Message += string.Format("Kliknite sledeći korak za upisivanje izmena u disk i završetka simulacije. ");
                        return true;
                    }

                    Message += string.Format("Prelazimo na baket sa adresom: {0}. ", bulkFile.OverrunZone[Row].Address);
                    Column = -1;

                    return true;
                }

                Record record = bulkFile.OverrunZone[Row].Records[Column];
                Message = string.Format("Proveravamo {0}. slog u baketu. ", Column + 1);
                if (record.Status != Status.empty && EditedRecord.Person.Id == record.Person.Id)
                {
                    Message += string.Format("Trenutni slog ima isti id kao novi slog! Unos sloga prekinut! ");
                    Column = -1;
                    Row = -1;

                    IsFinished = true;
                    Message += string.Format("Kliknite sledeći korak za upisivanje izmena u disk i završetka simulacije. ");
                    return true;
                }
                else if (record.Status != Status.empty)
                {
                    Message += string.Format("Trenutni slog je zauzet. Prelazimo na sledeći slog. ");
                    return true;
                }
                else if (record.Status == Status.empty)
                {
                    Message += string.Format("Trenutni slog je prazan. Upisujemo novi slog u trenutni slog i označavamo ga kao aktivan. ");

                    IsFinished = true;
                    Message += string.Format("Kliknite sledeći korak za upisivanje izmena u disk i završetka simulacije. ");
                    return true;
                }
            }

            return true;
        }
    }
}
