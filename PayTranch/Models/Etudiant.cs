namespace PayTranch.Models
{
    public class Etudiant : ApplicationUser
    {
        public string Matricule {  get; set; }
        public string Departement { get; set; }
        public Niveau Niveau { get; set; }
        public int Score { get; set; }
    }

    public enum Niveau
    {
        L1, L2, L3, M1, M2
    }
}
