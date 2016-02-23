
//  Microsoft (R) .NET Framework IL Disassembler.  Version 3.5.30729.1
//  Copyright (c) Microsoft Corporation.  All rights reserved.


// warning : THIS IS A PARTIAL DISASSEMBLY, NOT SUITABLE FOR RE-ASSEMBLING



// Metadata version: v2.0.50727
.assembly extern System
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 2:0:0:0
}
.assembly extern Microsoft.VisualBasic
{
  .publickeytoken = (B0 3F 5F 7F 11 D5 0A 3A )                         // .?_....:
  .ver 8:0:0:0
}
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 2:0:0:0
}
.assembly extern System.Windows.Forms
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 2:0:0:0
}
.assembly extern System.Drawing
{
  .publickeytoken = (B0 3F 5F 7F 11 D5 0A 3A )                         // .?_....:
  .ver 2:0:0:0
}
.assembly extern CCAPHelper
{
  .ver 1:0:0:0
}
.assembly WindowsFormsApplication1
{
  .hash algorithm 0x00008004
  .ver 1:0:0:0
}
.mresource public WindowsFormsApplication1.Form1.resources
{
  // Offset: 0x00000000 Length: 0x000000B4
  // WARNING: managed resource file WindowsFormsApplication1.Form1.resources created
}
.mresource public WindowsFormsApplication1.Properties.Resources.resources
{
  // Offset: 0x000000B8 Length: 0x000000B4
  // WARNING: managed resource file WindowsFormsApplication1.Properties.Resources.resources created
}
.module WindowsFormsApplication1.exe
// MVID: {28E08D0E-9368-4FA7-98BD-E0978A7677A1}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0002       // WINDOWS_GUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x012E0000


// =============== CLASS MEMBERS DECLARATION ===================

.class private auto ansi sealed beforefieldinit WindowsFormsApplication1.Properties.Settings
       extends [System]System.Configuration.ApplicationSettingsBase
{
  .field private static class WindowsFormsApplication1.Properties.Settings defaultInstance
  .method public hidebysig specialname static 
          class WindowsFormsApplication1.Properties.Settings 
          get_Default() cil managed
  {
    // Code size       11 (0xb)
    .maxstack  1
    .locals init ([0] class WindowsFormsApplication1.Properties.Settings CS$1$0000)
    IL_0000:  nop
    IL_0001:  ldsfld     class WindowsFormsApplication1.Properties.Settings WindowsFormsApplication1.Properties.Settings::defaultInstance
    IL_0006:  stloc.0
    IL_0007:  br.s       IL_0009

    IL_0009:  ldloc.0
    IL_000a:  ret
  } // end of method Settings::get_Default

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [System]System.Configuration.ApplicationSettingsBase::.ctor()
    IL_0006:  ret
  } // end of method Settings::.ctor

  .method private hidebysig specialname rtspecialname static 
          void  .cctor() cil managed
  {
    // Code size       21 (0x15)
    .maxstack  8
    IL_0000:  newobj     instance void WindowsFormsApplication1.Properties.Settings::.ctor()
    IL_0005:  call       class [System]System.Configuration.SettingsBase [System]System.Configuration.SettingsBase::Synchronized(class [System]System.Configuration.SettingsBase)
    IL_000a:  castclass  WindowsFormsApplication1.Properties.Settings
    IL_000f:  stsfld     class WindowsFormsApplication1.Properties.Settings WindowsFormsApplication1.Properties.Settings::defaultInstance
    IL_0014:  ret
  } // end of method Settings::.cctor

  .property class WindowsFormsApplication1.Properties.Settings
          Default()
  {
    .get class WindowsFormsApplication1.Properties.Settings WindowsFormsApplication1.Properties.Settings::get_Default()
  } // end of property Settings::Default
} // end of class WindowsFormsApplication1.Properties.Settings

.class public auto ansi beforefieldinit WindowsFormsApplication1.Program
       extends [Microsoft.VisualBasic]Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase
{
  .method private hidebysig static void  Main(string[] args) cil managed
  {
    .entrypoint
    // Code size       44 (0x2c)
    .maxstack  2
    .locals init ([0] class WindowsFormsApplication1.Program prg)
    IL_0000:  nop
    IL_0001:  newobj     instance void WindowsFormsApplication1.Program::.ctor()
    IL_0006:  stloc.0
    IL_0007:  ldloc.0
    IL_0008:  ldc.i4.1
    IL_0009:  callvirt   instance void [Microsoft.VisualBasic]Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase::set_EnableVisualStyles(bool)
    IL_000e:  nop
    IL_000f:  ldloc.0
    IL_0010:  ldc.i4.1
    IL_0011:  callvirt   instance void [Microsoft.VisualBasic]Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase::set_IsSingleInstance(bool)
    IL_0016:  nop
    IL_0017:  ldloc.0
    IL_0018:  newobj     instance void WindowsFormsApplication1.Form1::.ctor()
    IL_001d:  callvirt   instance void [Microsoft.VisualBasic]Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase::set_MainForm(class [System.Windows.Forms]System.Windows.Forms.Form)
    IL_0022:  nop
    IL_0023:  ldloc.0
    IL_0024:  ldarg.0
    IL_0025:  callvirt   instance void [Microsoft.VisualBasic]Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase::Run(string[])
    IL_002a:  nop
    IL_002b:  ret
  } // end of method Program::Main

  .method family hidebysig virtual instance void 
          OnStartupNextInstance(class [Microsoft.VisualBasic]Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs eventArgs) cil managed
  {
    // Code size       29 (0x1d)
    .maxstack  2
    .locals init ([0] class WindowsFormsApplication1.Form1 main)
    IL_0000:  nop
    IL_0001:  ldarg.0
    IL_0002:  call       instance class [System.Windows.Forms]System.Windows.Forms.Form [Microsoft.VisualBasic]Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase::get_MainForm()
    IL_0007:  isinst     WindowsFormsApplication1.Form1
    IL_000c:  stloc.0
    IL_000d:  ldloc.0
    IL_000e:  ldc.i4.0
    IL_000f:  callvirt   instance void [System.Windows.Forms]System.Windows.Forms.Form::set_WindowState(valuetype [System.Windows.Forms]System.Windows.Forms.FormWindowState)
    IL_0014:  nop
    IL_0015:  ldloc.0
    IL_0016:  callvirt   instance void [System.Windows.Forms]System.Windows.Forms.Control::BringToFront()
    IL_001b:  nop
    IL_001c:  ret
  } // end of method Program::OnStartupNextInstance

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [Microsoft.VisualBasic]Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase::.ctor()
    IL_0006:  ret
  } // end of method Program::.ctor

} // end of class WindowsFormsApplication1.Program

.class private auto ansi beforefieldinit WindowsFormsApplication1.Properties.Resources
       extends [mscorlib]System.Object
{
  .field private static class [mscorlib]System.Resources.ResourceManager resourceMan
  .field private static class [mscorlib]System.Globalization.CultureInfo resourceCulture
  .method assembly hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       10 (0xa)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  nop
    IL_0007:  nop
    IL_0008:  nop
    IL_0009:  ret
  } // end of method Resources::.ctor

  .method assembly hidebysig specialname static 
          class [mscorlib]System.Resources.ResourceManager 
          get_ResourceManager() cil managed
  {
    // Code size       60 (0x3c)
    .maxstack  3
    .locals init ([0] class [mscorlib]System.Resources.ResourceManager temp,
             [1] class [mscorlib]System.Resources.ResourceManager CS$1$0000,
             [2] bool CS$4$0001)
    IL_0000:  nop
    IL_0001:  ldsfld     class [mscorlib]System.Resources.ResourceManager WindowsFormsApplication1.Properties.Resources::resourceMan
    IL_0006:  ldnull
    IL_0007:  ceq
    IL_0009:  ldc.i4.0
    IL_000a:  ceq
    IL_000c:  stloc.2
    IL_000d:  ldloc.2
    IL_000e:  brtrue.s   IL_0032

    IL_0010:  nop
    IL_0011:  ldstr      "WindowsFormsApplication1.Properties.Resources"
    IL_0016:  ldtoken    WindowsFormsApplication1.Properties.Resources
    IL_001b:  call       class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
    IL_0020:  callvirt   instance class [mscorlib]System.Reflection.Assembly [mscorlib]System.Type::get_Assembly()
    IL_0025:  newobj     instance void [mscorlib]System.Resources.ResourceManager::.ctor(string,
                                                                                         class [mscorlib]System.Reflection.Assembly)
    IL_002a:  stloc.0
    IL_002b:  ldloc.0
    IL_002c:  stsfld     class [mscorlib]System.Resources.ResourceManager WindowsFormsApplication1.Properties.Resources::resourceMan
    IL_0031:  nop
    IL_0032:  ldsfld     class [mscorlib]System.Resources.ResourceManager WindowsFormsApplication1.Properties.Resources::resourceMan
    IL_0037:  stloc.1
    IL_0038:  br.s       IL_003a

    IL_003a:  ldloc.1
    IL_003b:  ret
  } // end of method Resources::get_ResourceManager

  .method assembly hidebysig specialname static 
          class [mscorlib]System.Globalization.CultureInfo 
          get_Culture() cil managed
  {
    // Code size       11 (0xb)
    .maxstack  1
    .locals init ([0] class [mscorlib]System.Globalization.CultureInfo CS$1$0000)
    IL_0000:  nop
    IL_0001:  ldsfld     class [mscorlib]System.Globalization.CultureInfo WindowsFormsApplication1.Properties.Resources::resourceCulture
    IL_0006:  stloc.0
    IL_0007:  br.s       IL_0009

    IL_0009:  ldloc.0
    IL_000a:  ret
  } // end of method Resources::get_Culture

  .method assembly hidebysig specialname static 
          void  set_Culture(class [mscorlib]System.Globalization.CultureInfo 'value') cil managed
  {
    // Code size       8 (0x8)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldarg.0
    IL_0002:  stsfld     class [mscorlib]System.Globalization.CultureInfo WindowsFormsApplication1.Properties.Resources::resourceCulture
    IL_0007:  ret
  } // end of method Resources::set_Culture

  .property class [mscorlib]System.Resources.ResourceManager
          ResourceManager()
  {
    .get class [mscorlib]System.Resources.ResourceManager WindowsFormsApplication1.Properties.Resources::get_ResourceManager()
  } // end of property Resources::ResourceManager
  .property class [mscorlib]System.Globalization.CultureInfo
          Culture()
  {
    .get class [mscorlib]System.Globalization.CultureInfo WindowsFormsApplication1.Properties.Resources::get_Culture()
    .set void WindowsFormsApplication1.Properties.Resources::set_Culture(class [mscorlib]System.Globalization.CultureInfo)
  } // end of property Resources::Culture
} // end of class WindowsFormsApplication1.Properties.Resources

.class public auto ansi beforefieldinit WindowsFormsApplication1.Form1
       extends [System.Windows.Forms]System.Windows.Forms.Form
{
  .field private class [System]System.ComponentModel.IContainer components
  .field private class [System.Windows.Forms]System.Windows.Forms.Button button1
  .method family hidebysig virtual instance void 
          Dispose(bool disposing) cil managed
  {
    // Code size       43 (0x2b)
    .maxstack  2
    .locals init ([0] bool CS$4$0000)
    IL_0000:  nop
    IL_0001:  ldarg.1
    IL_0002:  brfalse.s  IL_000f

    IL_0004:  ldarg.0
    IL_0005:  ldfld      class [System]System.ComponentModel.IContainer WindowsFormsApplication1.Form1::components
    IL_000a:  ldnull
    IL_000b:  ceq
    IL_000d:  br.s       IL_0010

    IL_000f:  ldc.i4.1
    IL_0010:  stloc.0
    IL_0011:  ldloc.0
    IL_0012:  brtrue.s   IL_0022

    IL_0014:  nop
    IL_0015:  ldarg.0
    IL_0016:  ldfld      class [System]System.ComponentModel.IContainer WindowsFormsApplication1.Form1::components
    IL_001b:  callvirt   instance void [mscorlib]System.IDisposable::Dispose()
    IL_0020:  nop
    IL_0021:  nop
    IL_0022:  ldarg.0
    IL_0023:  ldarg.1
    IL_0024:  call       instance void [System.Windows.Forms]System.Windows.Forms.Form::Dispose(bool)
    IL_0029:  nop
    IL_002a:  ret
  } // end of method Form1::Dispose

  .method private hidebysig instance void 
          InitializeComponent() cil managed
  {
    // Code size       308 (0x134)
    .maxstack  4
    IL_0000:  nop
    IL_0001:  ldarg.0
    IL_0002:  newobj     instance void [System.Windows.Forms]System.Windows.Forms.Button::.ctor()
    IL_0007:  stfld      class [System.Windows.Forms]System.Windows.Forms.Button WindowsFormsApplication1.Form1::button1
    IL_000c:  ldarg.0
    IL_000d:  call       instance void [System.Windows.Forms]System.Windows.Forms.Control::SuspendLayout()
    IL_0012:  nop
    IL_0013:  ldarg.0
    IL_0014:  ldfld      class [System.Windows.Forms]System.Windows.Forms.Button WindowsFormsApplication1.Form1::button1
    IL_0019:  ldc.i4.s   124
    IL_001b:  ldc.i4     0xa8
    IL_0020:  newobj     instance void [System.Drawing]System.Drawing.Point::.ctor(int32,
                                                                                   int32)
    IL_0025:  callvirt   instance void [System.Windows.Forms]System.Windows.Forms.Control::set_Location(valuetype [System.Drawing]System.Drawing.Point)
    IL_002a:  nop
    IL_002b:  ldarg.0
    IL_002c:  ldfld      class [System.Windows.Forms]System.Windows.Forms.Button WindowsFormsApplication1.Form1::button1
    IL_0031:  ldstr      "button1"
    IL_0036:  callvirt   instance void [System.Windows.Forms]System.Windows.Forms.Control::set_Name(string)
    IL_003b:  nop
    IL_003c:  ldarg.0
    IL_003d:  ldfld      class [System.Windows.Forms]System.Windows.Forms.Button WindowsFormsApplication1.Form1::button1
    IL_0042:  ldc.i4.s   75
    IL_0044:  ldc.i4.s   23
    IL_0046:  newobj     instance void [System.Drawing]System.Drawing.Size::.ctor(int32,
                                                                                  int32)
    IL_004b:  callvirt   instance void [System.Windows.Forms]System.Windows.Forms.Control::set_Size(valuetype [System.Drawing]System.Drawing.Size)
    IL_0050:  nop
    IL_0051:  ldarg.0
    IL_0052:  ldfld      class [System.Windows.Forms]System.Windows.Forms.Button WindowsFormsApplication1.Form1::button1
    IL_0057:  ldc.i4.0
    IL_0058:  callvirt   instance void [System.Windows.Forms]System.Windows.Forms.Control::set_TabIndex(int32)
    IL_005d:  nop
    IL_005e:  ldarg.0
    IL_005f:  ldfld      class [System.Windows.Forms]System.Windows.Forms.Button WindowsFormsApplication1.Form1::button1
    IL_0064:  ldstr      "button1"
    IL_0069:  callvirt   instance void [System.Windows.Forms]System.Windows.Forms.Control::set_Text(string)
    IL_006e:  nop
    IL_006f:  ldarg.0
    IL_0070:  ldfld      class [System.Windows.Forms]System.Windows.Forms.Button WindowsFormsApplication1.Form1::button1
    IL_0075:  ldc.i4.1
    IL_0076:  callvirt   instance void [System.Windows.Forms]System.Windows.Forms.ButtonBase::set_UseVisualStyleBackColor(bool)
    IL_007b:  nop
    IL_007c:  ldarg.0
    IL_007d:  ldfld      class [System.Windows.Forms]System.Windows.Forms.Button WindowsFormsApplication1.Form1::button1
    IL_0082:  ldarg.0
    IL_0083:  ldftn      instance void WindowsFormsApplication1.Form1::button1_Click(object,
                                                                                     class [mscorlib]System.EventArgs)
    IL_0089:  newobj     instance void [mscorlib]System.EventHandler::.ctor(object,
                                                                            native int)
    IL_008e:  callvirt   instance void [System.Windows.Forms]System.Windows.Forms.Control::add_Click(class [mscorlib]System.EventHandler)
    IL_0093:  nop
    IL_0094:  ldarg.0
    IL_0095:  ldc.r4     6.
    IL_009a:  ldc.r4     13.
    IL_009f:  newobj     instance void [System.Drawing]System.Drawing.SizeF::.ctor(float32,
                                                                                   float32)
    IL_00a4:  call       instance void [System.Windows.Forms]System.Windows.Forms.ContainerControl::set_AutoScaleDimensions(valuetype [System.Drawing]System.Drawing.SizeF)
    IL_00a9:  nop
    IL_00aa:  ldarg.0
    IL_00ab:  ldc.i4.1
    IL_00ac:  call       instance void [System.Windows.Forms]System.Windows.Forms.ContainerControl::set_AutoScaleMode(valuetype [System.Windows.Forms]System.Windows.Forms.AutoScaleMode)
    IL_00b1:  nop
    IL_00b2:  ldarg.0
    IL_00b3:  ldc.i4     0x124
    IL_00b8:  ldc.i4     0x111
    IL_00bd:  newobj     instance void [System.Drawing]System.Drawing.Size::.ctor(int32,
                                                                                  int32)
    IL_00c2:  call       instance void [System.Windows.Forms]System.Windows.Forms.Form::set_ClientSize(valuetype [System.Drawing]System.Drawing.Size)
    IL_00c7:  nop
    IL_00c8:  ldarg.0
    IL_00c9:  call       instance class [System.Windows.Forms]System.Windows.Forms.Control/ControlCollection [System.Windows.Forms]System.Windows.Forms.Control::get_Controls()
    IL_00ce:  ldarg.0
    IL_00cf:  ldfld      class [System.Windows.Forms]System.Windows.Forms.Button WindowsFormsApplication1.Form1::button1
    IL_00d4:  callvirt   instance void [System.Windows.Forms]System.Windows.Forms.Control/ControlCollection::Add(class [System.Windows.Forms]System.Windows.Forms.Control)
    IL_00d9:  nop
    IL_00da:  ldarg.0
    IL_00db:  ldstr      "Form1"
    IL_00e0:  call       instance void [System.Windows.Forms]System.Windows.Forms.Control::set_Name(string)
    IL_00e5:  nop
    IL_00e6:  ldarg.0
    IL_00e7:  ldstr      "Form1"
    IL_00ec:  callvirt   instance void [System.Windows.Forms]System.Windows.Forms.Control::set_Text(string)
    IL_00f1:  nop
    IL_00f2:  ldarg.0
    IL_00f3:  ldarg.0
    IL_00f4:  ldftn      instance void WindowsFormsApplication1.Form1::Form1_Load(object,
                                                                                  class [mscorlib]System.EventArgs)
    IL_00fa:  newobj     instance void [mscorlib]System.EventHandler::.ctor(object,
                                                                            native int)
    IL_00ff:  call       instance void [System.Windows.Forms]System.Windows.Forms.Form::add_Load(class [mscorlib]System.EventHandler)
    IL_0104:  nop
    IL_0105:  ldarg.0
    IL_0106:  ldarg.0
    IL_0107:  ldftn      instance void WindowsFormsApplication1.Form1::Form1_Shown(object,
                                                                                   class [mscorlib]System.EventArgs)
    IL_010d:  newobj     instance void [mscorlib]System.EventHandler::.ctor(object,
                                                                            native int)
    IL_0112:  call       instance void [System.Windows.Forms]System.Windows.Forms.Form::add_Shown(class [mscorlib]System.EventHandler)
    IL_0117:  nop
    IL_0118:  ldarg.0
    IL_0119:  ldarg.0
    IL_011a:  ldftn      instance void WindowsFormsApplication1.Form1::Form1_Resize(object,
                                                                                    class [mscorlib]System.EventArgs)
    IL_0120:  newobj     instance void [mscorlib]System.EventHandler::.ctor(object,
                                                                            native int)
    IL_0125:  call       instance void [System.Windows.Forms]System.Windows.Forms.Control::add_Resize(class [mscorlib]System.EventHandler)
    IL_012a:  nop
    IL_012b:  ldarg.0
    IL_012c:  ldc.i4.0
    IL_012d:  call       instance void [System.Windows.Forms]System.Windows.Forms.Control::ResumeLayout(bool)
    IL_0132:  nop
    IL_0133:  ret
  } // end of method Form1::InitializeComponent

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       24 (0x18)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  ldnull
    IL_0002:  stfld      class [System]System.ComponentModel.IContainer WindowsFormsApplication1.Form1::components
    IL_0007:  ldarg.0
    IL_0008:  call       instance void [System.Windows.Forms]System.Windows.Forms.Form::.ctor()
    IL_000d:  nop
    IL_000e:  nop
    IL_000f:  ldarg.0
    IL_0010:  call       instance void WindowsFormsApplication1.Form1::InitializeComponent()
    IL_0015:  nop
    IL_0016:  nop
    IL_0017:  ret
  } // end of method Form1::.ctor

  .method private hidebysig instance void 
          Form1_Load(object sender,
                     class [mscorlib]System.EventArgs e) cil managed
  {
    // Code size       2 (0x2)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ret
  } // end of method Form1::Form1_Load

  .method private hidebysig instance void 
          Form1_Shown(object sender,
                      class [mscorlib]System.EventArgs e) cil managed
  {
    // Code size       50 (0x32)
    .maxstack  2
    .locals init ([0] string handle,
             [1] native int CS$0$0000)
    IL_0000:  nop
    IL_0001:  call       class [System]System.Diagnostics.Process [System]System.Diagnostics.Process::GetCurrentProcess()
    IL_0006:  callvirt   instance native int [System]System.Diagnostics.Process::get_MainWindowHandle()
    IL_000b:  stloc.1
    IL_000c:  ldloca.s   CS$0$0000
    IL_000e:  constrained. [mscorlib]System.IntPtr
    IL_0014:  callvirt   instance string [mscorlib]System.Object::ToString()
    IL_0019:  stloc.0
    IL_001a:  ldstr      "windowhandle"
    IL_001f:  ldloc.0
    IL_0020:  call       void [CCAPHelper]CCAPHelper.CCAPJobHelper::WriteAppsettingToConfig(string,
                                                                                            string)
    IL_0025:  nop
    IL_0026:  ldstr      "Shown event fired"
    IL_002b:  call       valuetype [System.Windows.Forms]System.Windows.Forms.DialogResult [System.Windows.Forms]System.Windows.Forms.MessageBox::Show(string)
    IL_0030:  pop
    IL_0031:  ret
  } // end of method Form1::Form1_Shown

  .method private hidebysig instance void 
          button1_Click(object sender,
                        class [mscorlib]System.EventArgs e) cil managed
  {
    // Code size       10 (0xa)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldarg.0
    IL_0002:  ldc.i4.0
    IL_0003:  call       instance void [System.Windows.Forms]System.Windows.Forms.Control::set_Visible(bool)
    IL_0008:  nop
    IL_0009:  ret
  } // end of method Form1::button1_Click

  .method private hidebysig instance void 
          Form1_Resize(object sender,
                       class [mscorlib]System.EventArgs e) cil managed
  {
    // Code size       24 (0x18)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldarg.0
    IL_0002:  call       instance valuetype [System.Windows.Forms]System.Windows.Forms.FormWindowState [System.Windows.Forms]System.Windows.Forms.Form::get_WindowState()
    IL_0007:  box        [System.Windows.Forms]System.Windows.Forms.FormWindowState
    IL_000c:  callvirt   instance string [mscorlib]System.Object::ToString()
    IL_0011:  call       valuetype [System.Windows.Forms]System.Windows.Forms.DialogResult [System.Windows.Forms]System.Windows.Forms.MessageBox::Show(string)
    IL_0016:  pop
    IL_0017:  ret
  } // end of method Form1::Form1_Resize

} // end of class WindowsFormsApplication1.Form1


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file D:\Arun\Projects\Automation\CCAPQueue\CCAPQConsole\WindowsFormsApplication1\.sonar\ildasm_output.res
