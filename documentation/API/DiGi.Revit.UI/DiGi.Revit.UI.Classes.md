#### [DiGi\.Revit\.UI](DiGi.Revit.UI.Overview.md 'DiGi\.Revit\.UI\.Overview')

## DiGi\.Revit\.UI\.Classes Namespace
### Classes

<a name='DiGi.Revit.UI.Classes.ExternalApplication'></a>

## ExternalApplication Class

Revit add\-in bootstrap \([Autodesk\.Revit\.UI\.IExternalApplication](https://learn.microsoft.com/en-us/dotnet/api/autodesk.revit.ui.iexternalapplication 'Autodesk\.Revit\.UI\.IExternalApplication')\) that enables the DiGi assembly
resolver so managed and native assemblies deployed from a version\-specific Revit directory
load correctly\.

```csharp
public sealed class ExternalApplication
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [Autodesk\.Revit\.UI\.IExternalApplication](https://learn.microsoft.com/en-us/dotnet/api/autodesk.revit.ui.iexternalapplication 'Autodesk\.Revit\.UI\.IExternalApplication') → ExternalApplication
### Methods

<a name='DiGi.Revit.UI.Classes.ExternalApplication.OnShutdown(UIControlledApplication)'></a>

## ExternalApplication\.OnShutdown\(UIControlledApplication\) Method

Forwards shutdown to the external applications loaded during [OnStartup\(UIControlledApplication\)](DiGi.Revit.UI.Classes.md#DiGi.Revit.UI.Classes.ExternalApplication.OnStartup(UIControlledApplication) 'DiGi\.Revit\.UI\.Classes\.ExternalApplication\.OnStartup\(UIControlledApplication\)')\.

```csharp
public Result OnShutdown(UIControlledApplication application);
```
#### Parameters

<a name='DiGi.Revit.UI.Classes.ExternalApplication.OnShutdown(UIControlledApplication).application'></a>

`application` [Autodesk\.Revit\.UI\.UIControlledApplication](https://learn.microsoft.com/en-us/dotnet/api/autodesk.revit.ui.uicontrolledapplication 'Autodesk\.Revit\.UI\.UIControlledApplication')

The Revit application being shut down\.

#### Returns
[Autodesk\.Revit\.UI\.Result](https://learn.microsoft.com/en-us/dotnet/api/autodesk.revit.ui.result 'Autodesk\.Revit\.UI\.Result')  
[Autodesk\.Revit\.UI\.Result\.Succeeded](https://learn.microsoft.com/en-us/dotnet/api/autodesk.revit.ui.result.succeeded 'Autodesk\.Revit\.UI\.Result\.Succeeded')

<a name='DiGi.Revit.UI.Classes.ExternalApplication.OnStartup(UIControlledApplication)'></a>

## ExternalApplication\.OnStartup\(UIControlledApplication\) Method

Enables the DiGi assembly resolver with managed and native probe directories for the
running Revit version\.

```csharp
public Result OnStartup(UIControlledApplication application);
```
#### Parameters

<a name='DiGi.Revit.UI.Classes.ExternalApplication.OnStartup(UIControlledApplication).application'></a>

`application` [Autodesk\.Revit\.UI\.UIControlledApplication](https://learn.microsoft.com/en-us/dotnet/api/autodesk.revit.ui.uicontrolledapplication 'Autodesk\.Revit\.UI\.UIControlledApplication')

The Revit application being started\.

#### Returns
[Autodesk\.Revit\.UI\.Result](https://learn.microsoft.com/en-us/dotnet/api/autodesk.revit.ui.result 'Autodesk\.Revit\.UI\.Result')  
[Autodesk\.Revit\.UI\.Result\.Succeeded](https://learn.microsoft.com/en-us/dotnet/api/autodesk.revit.ui.result.succeeded 'Autodesk\.Revit\.UI\.Result\.Succeeded') when the resolver was enabled,
            [Autodesk\.Revit\.UI\.Result\.Cancelled](https://learn.microsoft.com/en-us/dotnet/api/autodesk.revit.ui.result.cancelled 'Autodesk\.Revit\.UI\.Result\.Cancelled') when the version\-specific directory is missing,
            [Autodesk\.Revit\.UI\.Result\.Failed](https://learn.microsoft.com/en-us/dotnet/api/autodesk.revit.ui.result.failed 'Autodesk\.Revit\.UI\.Result\.Failed') when the Revit version cannot be determined\.