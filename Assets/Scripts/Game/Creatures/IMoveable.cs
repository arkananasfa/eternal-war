using System.Collections;

public interface IMoveable
{

    int X { get; set; }
    int Y { get; set; }

    void Replace(int x, int y);
    void ReplaceTo(int x, int y);

}