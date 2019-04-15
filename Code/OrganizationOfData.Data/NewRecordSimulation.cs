namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;

    public class NewRecordSimulation
    {
        private BulkFileWithSerialOverrunZone bulkFile;

        public Record NewRecord { get; set; }

        public bool OverrunZone { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public string Message { get; set; }

        public NewRecordSimulation(BulkFileWithSerialOverrunZone bulkFile, Record newRecord)
        {
            this.bulkFile = bulkFile;
            this.NewRecord = newRecord;

            OverrunZone = false;
            Row = -1;
            Column = -1;
        }

        public bool NextStep()
        {
            if (OverrunZone == false)
            {
                if (Row == -1)
                {
                    int index = KeyTransformations.ResidualSplitting(NewRecord.Person.Id, bulkFile.NumberOfBuckets);

                    Message = string.Format("Radimo transformaciju ključa metodom 'neka metoda' i dobijamo adresu baketa: {0}. Ulazimo u taj baket", index);
                    Row = index;

                    return true;
                }

                Column++;

                if (Column >= this.bulkFile.Factor)
                {
                    OverrunZone = true;
                    Row = -1;
                    Column = -1;

                    Message = string.Format("Nema mesta u primarnoj zoni za uneti baket. Prelazimo u zonu prekoračenja.");

                    return true;
                }

                Record record = bulkFile.PrimaryZone[Row].Records[Column];
                Message = string.Format("Proveravamo slog sa id: {0}\n", record.Person.Id);
                if (record.Status != Status.empty && NewRecord.Person.Id == record.Person.Id)
                {
                    Message += string.Format("Trenutni slog ima isti id kao novi slog! Unos sloga prekinut!");
                    Column = -1;
                    Row = -1;
                    return false;
                }
                else if (record.Status == Status.empty)
                {
                    Message += string.Format("Trenutni slog je prazan. Upisujemo novi slog u trenutni slog i označavamo ga kao aktivan.");
                    return false;
                }
            }
            else
            {
                if (Row == -1)
                {
                    Row = 0;
                    Message = string.Format("Ulazimo u zonu prekoračenja.");
                }

                Column++;

                if (Column >= this.bulkFile.Factor)
                {
                    Message = "Došli smo do kraja trenutnog baketa";
                    Row++;

                    if (Row >= this.bulkFile.NumberOfBuckets)
                    {
                        Message = "Došli smo do kraja zone prekoračenja. Nema mesta u datoteci za dodavanje novog sloga sa unetim id";
                        Column = -1;
                        Row = -1;

                        return false;
                    }

                    Message += string.Format("Prelazimo na baket sa adresom: {0}", Column);

                    return true;
                }

                Record record = bulkFile.OverrunZone[Row].Records[Column];
                Message = string.Format("Proveravamo slog sa id: {0}\n", record.Person.Id);
                if (record.Status != Status.empty && NewRecord.Person.Id == record.Person.Id)
                {
                    Message += string.Format("Trenutni slog ima isti id kao novi slog! Unos sloga prekinut!");
                    Column = -1;
                    Row = -1;
                    return false;
                }
                else if (record.Status == Status.empty)
                {
                    Message += string.Format("Trenutni slog je prazan. Upisujemo novi slog u trenutni slog i označavamo ga kao aktivan.");
                    return false;
                }
            }

            return true;
        }
    }
}
