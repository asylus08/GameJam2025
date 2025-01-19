using Godot;

namespace GamejamV2.scripts;
public partial class Impact : Node
{
    public int commoner;
    public int clergy;
    public int army;

    public Impact(int commoner, int clergy, int army)   
    {
        this.commoner = commoner;   
        this.clergy = clergy;   
        this.army = army;
    }
}