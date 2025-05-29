Customer Management System
A comprehensive customer management system built with ASP.NET Core Razor Pages following MVVM architecture pattern.

🚀 Features
Customer List Management: Display customers with comprehensive information
Multi-field Search: Search across Name, Tel, Email, Note, and MetWhere fields
User-based Filtering: Only show customers created by the current user
Action Buttons: View, Purchase, and Join actions for each customer
Team Member Status: Visual indication of team membership
Responsive Design: Mobile-friendly Bootstrap interface

🔧 Setup Instructions
Prerequisites:
1. .NET 6.0 or later
2. SQL Server LocalDB and SSMS
3. Visual Studio 2022 

├── Models/
│   └── Customer.cs                    # Database entity model
├── ViewModels/
│   └── CustomerViewModel.cs           # View model for UI binding
├── Services/
│   ├── ICustomerService.cs           # Service interface
│   └── CustomerService.cs            # Service implementation
├── Repositories/
│   ├── ICustomerRepository.cs        # Repository interface
│   └── CustomerRepository.cs         # Repository implementation
└── Pages/Customers/
    ├── Index.cshtml                  # Customer list page
    └── Index.cshtml.cs              # Page model
