namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;

    public class NewRecordSimulation
    {
        private BulkFile bulkFile;
        private Record newRecord;

        public bool OverrunZone { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public string Message { get; set; }

        public NewRecordSimulation(BulkFile bulkFile, Record newRecord)
        {
            this.bulkFile = bulkFile;
            this.newRecord = newRecord;

            OverrunZone = false;
            Row = -1;
            Column = -1;
        }

        public bool NextStep()
        {
            if (Row == -1)
            {
                int index = KeyTransformations.ResidualSplitting(newRecord.Person.Id, bulkFile.NumberOfBuckets);

                Message = string.Format("Radimo transformaciju ključa metodom 'neka metoda' i dobijamo adresu baketa: {0}.\n Ulazimo u taj baket\n", index);
                Row = index;

                return true;
            }

            Column++;
            Record record = bulkFile.PrimaryZone[Row].Records[Column];
            Message = string.Format("Proveravamo slog sa id: {0}\n", record.Person.Id);
            if (record.Status != Status.empty && newRecord.Person.Id == record.Person.Id)
            {
                Message += string.Format("Trenutni slog ima isti id kao novi slog! Unos sloga prekinut!");
                return false;
            }

            return true;
        }
    }
}
