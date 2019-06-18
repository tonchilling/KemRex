# Generate Entity Framework Core

1. Command line for scaffold database
   ```cmd
   Scaffold-DbContext "Server=labs.dmnsn.com,9225;initial catalog=KEMREX_DEV;persist security info=True;user id=kemrex;password=asdf1234;" Microsoft.EntityFrameworkCore.SqlServer -f -Context mainContext -ContextDir ./ -Output ./Models
   ```

2. Change mainContext namespace & ref Models namespace
    ```C#
    using Kemrex.Core.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using System;
    
    namespace Kemrex.Core.Database
    ```
    
3. Replace default constructor & add property for connection string

    ```C#
    private readonly string constr;
    private mainContext()
    {
    }

    public mainContext(string _constr)
    {
		constr = _constr;
    }

    public mainContext(DbContextOptions<mainContext> options) : base(options)
    {
    }

    public mainContext(string _constr, DbContextOptions<mainContext> options) : base(options)
    {
		constr = _constr;
    }
    ```

4. Use connection string from constructor

    ```C#
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        { optionsBuilder.UseSqlServer(constr); }
    }
    ```
---

