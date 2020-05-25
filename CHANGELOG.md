# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.1.0] - 2020-03-27
### Added
- DebugUtility to log error and warnings in Editor only.
- A simple Statemachine, examples will follow.
- A simple Healthsystem / Barsystem.
- A constant acceleration script (Rigidbody Velocity).
- A constant velocity script (Rigidbody Velocity).

## [0.2.0] - 2020-03-31
### Added
- Vector3 extensions.
- Vector2 extensions.
- Transform extensions based on the Vector3 extensions.
- GameObject extensions based on the Vector3 extensions.
- SpriteRenederer extensions.
- A simple prefab spawner script.
- A simple DestroyOnCollision script.

### Changed
- AddConstantVelocity.cs - Changed default member variables values.
- SetConstantVelocity.cs - Changed default member variables values.
- Health.cs - commented unused members variables.
- EnergyShield.cs  - commented unused members variables.

## [0.2.1] - 2020-04-22
### Added
- CameraShaderEffect.cs, gives an easy way to attach a camera shader to your project.
- CRT scanline camera shader
- Vignette camera shader
### Removed
- RotationTo() extensions
- LocalRotation() extensions

## [0.2.2] - 2020-04-23
### Added
- SceneLoader.cs - easy way to load scenes additive with progression values

### Changed
- CameraShaderEffect.cs - code clean-up

## [0.2.3] - 2020-05-25
### Added
- CheckIfComponentIsSet<T>() - easy way check if component on script is set or NULL, returns false when NULL.

### Changed
- DebugUtility.cs - code clean-up






