# Unity Texture Pipeline Validator

A lightweight Unity Editor tool for validating texture assets in a mobile game production pipeline.

This tool was created as a Technical Art pipeline automation example focused on improving workflow usability, reducing repetitive QA checks, and helping content creators identify common texture import issues directly inside Unity.

---

# Features

- Validate selected textures
- Validate entire folders
- Batch validation support
- Mobile texture size checks
- Compression validation
- Context menu integration
- In-editor validation results window

---

# Why This Tool Exists

In mobile game production, incorrect texture import settings can quickly create memory, performance, and production issues.

This tool helps artists and technical artists quickly identify common problems before assets enter production pipelines.

The project was designed as a small example of:
- Unity Editor tooling
- Pipeline automation
- Workflow optimization
- Technical Art support tooling

---

# Example Validation Checks

The validator currently detects:

- Oversized textures
- Missing texture compression

Example warnings:

```text
⚠ T_Block_Grass_Bad is too large: 4096x4096
⚠ T_Block_Grass_Bad uses no compression

---

# Included Example Assets

The repository includes simple example textures for testing validation workflows.

## Good Example

`ExampleTextures/Good/T_Block_Grass_Good.png`

- Mobile-friendly example texture
- Intended to pass validation checks

## Bad Example

`ExampleTextures/Bad/T_Block_Grass_Bad.png`

- Intentionally oversized texture example
- Intended to trigger validation warnings
- Useful for testing pipeline validation workflows

---

# How To Use

1. Select textures or folders inside the Unity Project window
2. Right click the selection
3. Choose:

```text
Pipeline Tools → Validate Textures
```

4. Review validation results in the popup window
