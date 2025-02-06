public class Actor
{
    public int ID;
    public string name;
    public string title;
    public string weaponName;
    public float strength;
    public int level;

    public string Talk()
    { return "Started a talk"; }

    public string HasWeapon()
    { return weaponName; }

    public void LevelUp()
    { level += 1; }
}