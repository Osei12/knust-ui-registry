namespace __MYUI_NAMESPACE__;

public class CssBuilder
{
    private string _class;
    public CssBuilder(string baseClass) { _class = baseClass; }
    public CssBuilder AddClass(string? newClass) 
    { 
        if (!string.IsNullOrEmpty(newClass)) 
            _class += $" {newClass}"; 
        return this; 
    }
    public CssBuilder AddClass(string? newClass, bool condition) => condition ? AddClass(newClass) : this;
    public string Build() => _class;
}
