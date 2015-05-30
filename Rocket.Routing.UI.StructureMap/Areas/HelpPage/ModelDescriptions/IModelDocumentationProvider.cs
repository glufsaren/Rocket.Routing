using System;
using System.Reflection;

namespace Rocket.Routing.UI.StructureMap.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}