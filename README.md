# BulletSharp
BulletSharp is a .NET wrapper for the [Bullet](https://pybullet.org/) physics library.

This version uses Platform Invoke. There is also an equivalent version written in C++/CLI: https://github.com/AndresTraks/BulletSharp

libbulletc is a C interface to Bullet. It compiles into a .dll or .so file that exports Bullet functions.

BulletSharpPInvoke is a .NET library that proxies calls from .NET to libbulletc.

The benefit of P/Invoke over C++/CLI is that it runs on all platforms that support P/Invoke into shared user-mode libraries (Windows, Unix, Mac OS). See also [Supported platforms](https://github.com/AndresTraks/BulletSharp/wiki/Supported-platforms).

![.NET Core](https://github.com/AndresTraks/BulletSharpPInvoke/workflows/.NET%20Core/badge.svg)


## Build instructions

### Prerequisites
 * CMake
 * CMake-compatible C++ compiler (Visual Studio, GCC, etc.)
 * C# compiler (Visual Studio or Mono C# Compiler)

### Instructions

#### 1. Fetch Bullet and BulletSharp
 * git clone **https://github.com/bulletphysics/bullet3**
 * git clone **https://github.com/AndresTraks/BulletSharpPInvoke**

#### 2. Generate libbulletc solution files with CMake
 * Set the **source code** directory to BulletSharpPInvoke/libbulletc
 * Set the **build** directory to e.g. BulletSharpPInvoke/libbulletc/build

 You can change these options:
 * **Optional platform for generator** - x64, Win32
 * **BULLET_INCLUDE_DIR** - the location of bullet3/src (autodetected)
 * **USE_DOUBLE_PRECISION** - use with the [double precision branch](https://github.com/AndresTraks/BulletSharpPInvoke/tree/double-precision)
 * **BULLET2_MULTITHREADING** - to enable multithreading, select BULLET2_MULTITHREADING, press Configure and then select available libraries: **BULLET2_USE_PPL_MULTITHREADING**, **BULLET2_USE_OPEN_MP_MULTITHREADING**, **BULLET2_USE_TBB_MULTITHREADING**

#### 3. Build libbulletc
 * On Windows, open **BulletSharpPInvoke/libbulletc/build/libbulletc.sln** and build the solution
 * On Unix, open a terminal at **BulletSharpPInvoke/libbulletc/build** and run "**make**"

#### 4. Build BulletSharp
 * Open **BulletSharpPInvoke/BulletSharp/BulletSharpPInvoke.sln** and build the solution
