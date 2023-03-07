namespace Cgpe.Du.Domain.Entities
{

    public class ProcuratorSituation
    {

        public ProcuratorSituationEnum ProcuratorSituationId { get; set; }
       public string ProcuratorSituationName { get; set; }

        public bool CheckIfIsPractising()
        {
                return this.ProcuratorSituationId == ProcuratorSituationEnum.Practising
                    || this.ProcuratorSituationId == ProcuratorSituationEnum.UnregisteredTemporarily;
        }
    }
}
