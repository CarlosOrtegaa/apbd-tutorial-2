namespace EquipmentRentalApp.Models;

public class Camera : Equipment
{
    public string Resolution { get; }
    public bool HasOpticalZoom { get; }

    public Camera(string name, string resolution, bool hasOpticalZoom) : base(name)
    {
        Resolution = resolution;
        HasOpticalZoom = hasOpticalZoom;
    }

    public override string GetDetails()
    {
        return $"Camera | Id: {Id}, Name: {Name}, Resolution: {Resolution}, Optical Zoom: {HasOpticalZoom}, Status: {StatusNote}";
    }
}