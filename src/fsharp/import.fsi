// Copyright (c) Microsoft Open Technologies, Inc.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

module internal Microsoft.FSharp.Compiler.Import

open Microsoft.FSharp.Compiler.Tast
open Microsoft.FSharp.Compiler.Range
open Microsoft.FSharp.Compiler.AbstractIL.IL
#if EXTENSIONTYPING
open Microsoft.FSharp.Compiler.ExtensionTyping
#endif



/// Represents an interface to some of the functionality of TcImports, for loading assemblies 
/// and accessing information about generated provided assemblies.
type AssemblyLoader = 

    /// Resolve an Abstract IL assembly reference to a Ccu
    abstract LoadAssembly : range * ILAssemblyRef -> CcuResolutionResult

#if EXTENSIONTYPING
    /// Get a flag indicating if an assembly is a provided assembly, plus the
    /// table of information recording remappings from type names in the provided assembly to type
    /// names in the statically linked, embedded assembly.
    abstract GetProvidedAssemblyInfo : range * Tainted<ProvidedAssembly> -> bool * ProvidedAssemblyStaticLinkingMap option

    /// Record a root for a [<Generate>] type to help guide static linking & type relocation
    abstract RecordGeneratedTypeRoot : ProviderGeneratedType -> unit
#endif


/// Represents a context used for converting AbstractIL .NET and provided types to F# internal compiler data structures.
/// Also cache the conversion of AbstractIL ILTypeRef nodes, based on hashes of these.
///
/// There is normally only one ImportMap for any assembly compilation, though additional instances can be created
/// using tcImports.GetImportMap() if needed, and it is not harmful if multiple instances are used. The object 
/// serves as an interface through to the tables stored in the primary TcImports structures defined in build.fs. 
[<SealedAttribute>]
type ImportMap =
    new : g:Env.TcGlobals * assemblyLoader:AssemblyLoader -> ImportMap
    
    /// The AssemblyLoader for the import context
    member assemblyLoader : AssemblyLoader

    /// The TcGlobals for the import context
    member g : Env.TcGlobals

/// Import a reference to a type definition, given an AbstractIL ILTypeRef, with caching
val internal ImportILTypeRef : ImportMap -> range -> ILTypeRef -> TyconRef

/// Import an IL type as an F# type.
val internal ImportILType : ImportMap -> range -> TType list -> ILType -> TType

#if EXTENSIONTYPING

/// Import a provided type as an F# type.
val internal ImportProvidedType : ImportMap -> range -> (* TType list -> *) Tainted<ProvidedType> -> TType

/// Import a provided type reference as an F# type TyconRef
val internal ImportProvidedNamedType : ImportMap -> range -> (* TType list -> *) Tainted<ProvidedType> -> TyconRef

/// Import a provided type as an AbstractIL type
val internal ImportProvidedTypeAsILType : ImportMap -> range -> Tainted<ProvidedType> -> ILType

/// Import a provided method reference as an Abstract IL method reference
val internal ImportProvidedMethodBaseAsILMethodRef : ImportMap -> range -> Tainted<ProvidedMethodBase> -> ILMethodRef
#endif

/// Import a set of Abstract IL generic parameter specifications as a list of new F# generic parameters.  
val internal ImportILGenericParameters : (unit -> ImportMap) -> range -> ILScopeRef -> TType list -> ILGenericParameterDef list -> Typar list

/// Import an IL assembly as a new TAST CCU
val internal ImportILAssembly : (unit -> ImportMap) * range * (ILScopeRef -> ILModuleDef) * ILScopeRef * sourceDir:string * filename: string option * ILModuleDef * IEvent<string> -> CcuThunk

/// Import the type forwarder table for an IL assembly
val internal ImportILAssemblyTypeForwarders : (unit -> ImportMap) * range * ILExportedTypesAndForwarders -> Map<(string array * string), Lazy<EntityRef>>