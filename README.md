## ActiveDirectory.Core
[![Build Status](https://travis-ci.com/FewBox/ActiveDirectory.Core.svg?branch=master)](https://travis-ci.com/FewBox/ActiveDirectory.Core)
[![Nuget Status](https://img.shields.io/nuget/v/FewBox.ActiveDirectory.Core.svg)](https://www.nuget.org/packages/FewBox.ActiveDirectory.Core/)

.Net Core Active Directory Lib.
> Start from 2010 (https://landpyactivedirectory.codeplex.com/), which will help you to manage Active Directory(LDAP protocol) with out complex filter syntax!
This library has been trusted by Lenovo, Sony，Tempursealy, BoostSolutions and other corporations.
> This library has been trusted by **Lenovo**, **Sony**，**Tempursealy**, **BoostSolutions** and other corporations.

[Getting Start](https://github.com/fewbox/ActiveDirectory.Core/wiki/Getting-Start)

**Note** - Must init the context first:

**ClientContext.Init("LDAP://222.222.222.222", "delegate", "1qaz@WSX");**

E.G: Update a user AD object.

    using (var userObject = UserObject.FindOneByCN(this.ADOperator, “pangxiaoliang”))
    {
         if(userObject.Email == "example@landpy.com")``
         {
              userObject.Email = "marketing@fewbox.com";``
              userObject.Save();``
         }
    }

E.G: Query user AD objects.

    // 1. CN end with "liu", Mail conatains "live" (Eg: marketing@fewbox.com),
    // job title is "Dev" and AD object type is user.
    // 2. CN start with "pang", Mail conatains "live" (Eg: marketing@fewbox.com),
    // job title is "Dev" and AD object type is user.
                IFilter filter =
                    new And(
                        new IsUser(),
                        new Contains(PersonAttributeNames.Mail, "live"),
                        new Is(PersonAttributeNames.Title, "Dev"),
                        new Or(
                                new StartWith(AttributeNames.CN, "pang"),
                                new EndWith(AttributeNames.CN, "liu")
                            )
                        );
    // Output the user object display name.
    foreach (var userObject in UserObject.FindAll(this.ADOperator, filter))
    {
        using (userObject)
        {
            Console.WriteLine(userObject.DisplayName);
        }
    }

E.G: Custom query.

    IFilter filter =
        new And(
            new IsUser(),
            new Custom("(!userAccountControl:1.2.840.113556.1.4.803:=2)")
            );
    // Output the user object display name.
    foreach (var userObject in UserObject.FindAll(this.ADOperator, filter))
    {
        using (userObject)
        {
            Console.WriteLine(userObject.DisplayName);
        }
    }
