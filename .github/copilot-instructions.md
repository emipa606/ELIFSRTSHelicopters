# GitHub Copilot Instructions for [ELIF] SRTS - Helicopters (Continued)

## Mod Overview and Purpose
The [ELIF] SRTS - Helicopters (Continued) mod brings the utility and style of helicopters to RimWorld, allowing players to engage in air operations without the need for a large runway. Designed to integrate smoothly into the late game, this mod introduces a range of specialized helicopters each tailored for different strategic uses, including bombing and transport missions.

## Key Features and Systems
- **Helicopter Varieties**: Four distinct types of helicopters each with unique functionalities ranging from strategic bombing to long-range fast deployment.
- **Realistic Propeller Mechanics**: Detailed spinning top and tail propellers that engage and disengage during flight transitions.
- **Dynamic Visual Effects**: Dust and dirt effects emitted when rotors are spinning, adding a visual element to helicopter operations.
- **Translations**: Support for multiple translations including Chinese, Russian, and Japanese.
- **Compatibility**: Tested compatibility with mods like AIRDOCK, with noted incompatibilities with Combat Extended.

## Coding Patterns and Conventions
- **Class Naming**: Use PascalCase for class names like `CompProps_Rotor`, `RotorComponent`, and `RotorDef`.
- **Method Organization**: Keep related methods grouped within their respective classes for logical organization.
- **File Layout**: Each class is contained in its own file for clarity and maintainability.

## XML Integration
- **XML Configuration**: Define helicopter properties and behaviors in XML files, linking them with C# components for dynamic interactions.
- **Mod Extension**: Use XML to extend and override base game classes, enabling custom helicopter functionality.

## Harmony Patching
- **Non-Destructive Patching**: Use Harmony to patch methods when altering RimWorld's core behaviors. Ensure all patches are reversible and non-destructive to maintain game integrity.
- **Target Specific Methods**: Apply patches only to the necessary functions to minimize impact on performance and maintain compatibility with other mods.

## Suggestions for Copilot
- **Generate Method Stubs**: Use Copilot to quickly generate stub methods for your classes (`CompProps_Rotor`, `RotorComponent`, etc.) to streamline development.
- **Pattern Recognition**: Identify and suggest consistent method patterns based on existing codebase structures.
- **XML Annotation**: Help suggest and auto-complete XML tags and structures based on existing mod conventions and required fields.
- **Harmony Patches**: Aid in drafting Harmony patches by suggesting method signatures and potential patches for specific game methods.

---

For more information on contributing to this project or troubleshooting issues, reach out to the community via our Discord channel. Be sure to use appropriate logging tools such as RimSort to identify and resolve errors efficiently.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.
