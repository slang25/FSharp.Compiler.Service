// #Regression #Conformance #Regression 
#light
#nowarn "49";;
#nowarn "44";;
#nowarn "62";;
#nowarn "35";;

//
// The tests in this file used to be in ..\numbers\test.ml, but I moved them here since
// they do not work against Dev10: BigInteger.Pow() does not have the (BigInteger,BigInteger) -> BigInteger
// overload in NetFx4.0
// *** DO NOT ADD TESTS THAT WORK ON BOTH DEV10 and VS2008 TO THIS FILE ***
//

let failures = ref []

let report_failure (s : string) = 
    stderr.Write" NO: "
    stderr.WriteLine s
    failures := !failures @ [s]

let test (s : string) b = 
    stderr.Write(s)
    if b then stderr.WriteLine " OK"
    else report_failure (s)

let check s b1 b2 = test s (b1 = b2)

(* START *)

// Misc construction.
open Microsoft.FSharp.Math
open System.Numerics
let fail() = report_failure("Failed")
let checkEq desc a b = if a<>b then printf "Failed %s. %A <> %A\n" desc a b; fail()

// Regression 3481: Tables
  
// 2^n-1,2^n,2^n+1
let positive32s =
        [(0          , 1          , 2);
         (1          , 2          , 3);
         (3          , 4          , 5);
         (7          , 8          , 9);
         (15         , 16         , 17);
         (31         , 32         , 33);
         (63         , 64         , 65);
         (127        , 128        , 129);
         (255        , 256        , 257);
         (511        , 512        , 513);
         (1023       , 1024       , 1025);
         (2047       , 2048       , 2049);
         (4095       , 4096       , 4097);
         (8191       , 8192       , 8193);
         (16383      , 16384      , 16385);
         (32767      , 32768      , 32769);
         (65535      , 65536      , 65537);
         (131071     , 131072     , 131073);
         (262143     , 262144     , 262145);
         (524287     , 524288     , 524289);
         (1048575    , 1048576    , 1048577);
         (2097151    , 2097152    , 2097153);
         (4194303    , 4194304    , 4194305);
         (8388607    , 8388608    , 8388609);
         (16777215   , 16777216   , 16777217);
         (33554431   , 33554432   , 33554433);
         (67108863   , 67108864   , 67108865);
         (134217727  , 134217728  , 134217729);
         (268435455  , 268435456  , 268435457);
         (536870911  , 536870912  , 536870913);
         (1073741823 , 1073741824 , 1073741825);
         (2147483647 , 999        , 999); (* MaxValue is 2^31-1 *)
         (999        , 999        , 999)]

// -2^n-1,-2^n,-2^n+1
let negative32s =
        [(-2          , -1          , 0);
         (-3          , -2          , -1);
         (-5          , -4          , -3);
         (-9          , -8          , -7);
         (-17         , -16         , -15);
         (-33         , -32         , -31);
         (-65         , -64         , -63);
         (-129        , -128        , -127);
         (-257        , -256        , -255);
         (-513        , -512        , -511);
         (-1025       , -1024       , -1023);
         (-2049       , -2048       , -2047);
         (-4097       , -4096       , -4095);
         (-8193       , -8192       , -8191);
         (-16385      , -16384      , -16383);
         (-32769      , -32768      , -32767);
         (-65537      , -65536      , -65535);
         (-131073     , -131072     , -131071);
         (-262145     , -262144     , -262143);
         (-524289     , -524288     , -524287);
         (-1048577    , -1048576    , -1048575);
         (-2097153    , -2097152    , -2097151);
         (-4194305    , -4194304    , -4194303);
         (-8388609    , -8388608    , -8388607);
         (-16777217   , -16777216   , -16777215);
         (-33554433   , -33554432   , -33554431);
         (-67108865   , -67108864   , -67108863);
         (-134217729  , -134217728  , -134217727);
         (-268435457  , -268435456  , -268435455);
         (-536870913  , -536870912  , -536870911);
         (-1073741825 , -1073741824 , -1073741823);
         (999         , -2147483648 , -2147483647); (* MinValue is -2^31 *)
         (999         , 999         , 999)]  

// 2^n-1,2^n,2^n+1
let positive64s =
        [(0L                   , 1L                   , 2L);
         (1L                   , 2L                   , 3L);
         (3L                   , 4L                   , 5L);
         (7L                   , 8L                   , 9L);
         (15L                  , 16L                  , 17L);
         (31L                  , 32L                  , 33L);
         (63L                  , 64L                  , 65L);
         (127L                 , 128L                 , 129L);
         (255L                 , 256L                 , 257L);
         (511L                 , 512L                 , 513L);
         (1023L                , 1024L                , 1025L);
         (2047L                , 2048L                , 2049L);
         (4095L                , 4096L                , 4097L);
         (8191L                , 8192L                , 8193L);
         (16383L               , 16384L               , 16385L);
         (32767L               , 32768L               , 32769L);
         (65535L               , 65536L               , 65537L);
         (131071L              , 131072L              , 131073L);
         (262143L              , 262144L              , 262145L);
         (524287L              , 524288L              , 524289L);
         (1048575L             , 1048576L             , 1048577L);
         (2097151L             , 2097152L             , 2097153L);
         (4194303L             , 4194304L             , 4194305L);
         (8388607L             , 8388608L             , 8388609L);
         (16777215L            , 16777216L            , 16777217L);
         (33554431L            , 33554432L            , 33554433L);
         (67108863L            , 67108864L            , 67108865L);
         (134217727L           , 134217728L           , 134217729L);
         (268435455L           , 268435456L           , 268435457L);
         (536870911L           , 536870912L           , 536870913L);
         (1073741823L          , 1073741824L          , 1073741825L);
         (2147483647L          , 2147483648L          , 2147483649L);
         (4294967295L          , 4294967296L          , 4294967297L);
         (8589934591L          , 8589934592L          , 8589934593L);
         (17179869183L         , 17179869184L         , 17179869185L);
         (34359738367L         , 34359738368L         , 34359738369L);
         (68719476735L         , 68719476736L         , 68719476737L);
         (137438953471L        , 137438953472L        , 137438953473L);
         (274877906943L        , 274877906944L        , 274877906945L);
         (549755813887L        , 549755813888L        , 549755813889L);
         (1099511627775L       , 1099511627776L       , 1099511627777L);
         (2199023255551L       , 2199023255552L       , 2199023255553L);
         (4398046511103L       , 4398046511104L       , 4398046511105L);
         (8796093022207L       , 8796093022208L       , 8796093022209L);
         (17592186044415L      , 17592186044416L      , 17592186044417L);
         (35184372088831L      , 35184372088832L      , 35184372088833L);
         (70368744177663L      , 70368744177664L      , 70368744177665L);
         (140737488355327L     , 140737488355328L     , 140737488355329L);
         (281474976710655L     , 281474976710656L     , 281474976710657L);
         (562949953421311L     , 562949953421312L     , 562949953421313L);
         (1125899906842623L    , 1125899906842624L    , 1125899906842625L);
         (2251799813685247L    , 2251799813685248L    , 2251799813685249L);
         (4503599627370495L    , 4503599627370496L    , 4503599627370497L);
         (9007199254740991L    , 9007199254740992L    , 9007199254740993L);
         (18014398509481983L   , 18014398509481984L   , 18014398509481985L);
         (36028797018963967L   , 36028797018963968L   , 36028797018963969L);
         (72057594037927935L   , 72057594037927936L   , 72057594037927937L);
         (144115188075855871L  , 144115188075855872L  , 144115188075855873L);
         (288230376151711743L  , 288230376151711744L  , 288230376151711745L);
         (576460752303423487L  , 576460752303423488L  , 576460752303423489L);
         (1152921504606846975L , 1152921504606846976L , 1152921504606846977L);
         (2305843009213693951L , 2305843009213693952L , 2305843009213693953L);
         (4611686018427387903L , 4611686018427387904L , 4611686018427387905L);
         (9223372036854775807L , 999L                 , 999L); (* MaxValue is 2^63-1 *)
         (999L                 , 999L                 , 999L)]  

// -2^n-1,-2^n,-2^n+1
let negative64s =
        [(-2L                   , -1L                   , 0L);
         (-3L                   , -2L                   , -1L);
         (-5L                   , -4L                   , -3L);
         (-9L                   , -8L                   , -7L);
         (-17L                  , -16L                  , -15L);
         (-33L                  , -32L                  , -31L);
         (-65L                  , -64L                  , -63L);
         (-129L                 , -128L                 , -127L);
         (-257L                 , -256L                 , -255L);
         (-513L                 , -512L                 , -511L);
         (-1025L                , -1024L                , -1023L);
         (-2049L                , -2048L                , -2047L);
         (-4097L                , -4096L                , -4095L);
         (-8193L                , -8192L                , -8191L);
         (-16385L               , -16384L               , -16383L);
         (-32769L               , -32768L               , -32767L);
         (-65537L               , -65536L               , -65535L);
         (-131073L              , -131072L              , -131071L);
         (-262145L              , -262144L              , -262143L);
         (-524289L              , -524288L              , -524287L);
         (-1048577L             , -1048576L             , -1048575L);
         (-2097153L             , -2097152L             , -2097151L);
         (-4194305L             , -4194304L             , -4194303L);
         (-8388609L             , -8388608L             , -8388607L);
         (-16777217L            , -16777216L            , -16777215L);
         (-33554433L            , -33554432L            , -33554431L);
         (-67108865L            , -67108864L            , -67108863L);
         (-134217729L           , -134217728L           , -134217727L);
         (-268435457L           , -268435456L           , -268435455L);
         (-536870913L           , -536870912L           , -536870911L);
         (-1073741825L          , -1073741824L          , -1073741823L);
         (-2147483649L          , -2147483648L          , -2147483647L);
         (-4294967297L          , -4294967296L          , -4294967295L);
         (-8589934593L          , -8589934592L          , -8589934591L);
         (-17179869185L         , -17179869184L         , -17179869183L);
         (-34359738369L         , -34359738368L         , -34359738367L);
         (-68719476737L         , -68719476736L         , -68719476735L);
         (-137438953473L        , -137438953472L        , -137438953471L);
         (-274877906945L        , -274877906944L        , -274877906943L);
         (-549755813889L        , -549755813888L        , -549755813887L);
         (-1099511627777L       , -1099511627776L       , -1099511627775L);
         (-2199023255553L       , -2199023255552L       , -2199023255551L);
         (-4398046511105L       , -4398046511104L       , -4398046511103L);
         (-8796093022209L       , -8796093022208L       , -8796093022207L);
         (-17592186044417L      , -17592186044416L      , -17592186044415L);
         (-35184372088833L      , -35184372088832L      , -35184372088831L);
         (-70368744177665L      , -70368744177664L      , -70368744177663L);
         (-140737488355329L     , -140737488355328L     , -140737488355327L);
         (-281474976710657L     , -281474976710656L     , -281474976710655L);
         (-562949953421313L     , -562949953421312L     , -562949953421311L);
         (-1125899906842625L    , -1125899906842624L    , -1125899906842623L);
         (-2251799813685249L    , -2251799813685248L    , -2251799813685247L);
         (-4503599627370497L    , -4503599627370496L    , -4503599627370495L);
         (-9007199254740993L    , -9007199254740992L    , -9007199254740991L);
         (-18014398509481985L   , -18014398509481984L   , -18014398509481983L);
         (-36028797018963969L   , -36028797018963968L   , -36028797018963967L);
         (-72057594037927937L   , -72057594037927936L   , -72057594037927935L);
         (-144115188075855873L  , -144115188075855872L  , -144115188075855871L);
         (-288230376151711745L  , -288230376151711744L  , -288230376151711743L);
         (-576460752303423489L  , -576460752303423488L  , -576460752303423487L);
         (-1152921504606846977L , -1152921504606846976L , -1152921504606846975L);
         (-2305843009213693953L , -2305843009213693952L , -2305843009213693951L);
         (-4611686018427387905L , -4611686018427387904L , -4611686018427387903L);
         (999L                  , -9223372036854775808L , -9223372036854775807L); (* MinValue is -2^63 *)
         (999L                  , 999L                  , 999L)]

// Regression 3481: ToInt32
let triple k n project =
  let x = k * BigInteger.Pow(2I,n) in project (x - 1I),project x,project (x + 1I)
      
let triple32 k n = triple k n (fun x -> try int32 x with :? System.OverflowException -> 999)
let triple64 k n = triple k n (fun x -> try int64 x with :? System.OverflowException -> 999L)
          
printf "Checking BigInt ToInt32 and ToInt64\n"
checkEq "BigInt.ToInt32 positives" positive32s (List.map (triple32  1I) [0 .. 32])
checkEq "BigInt.ToInt32 negatives" negative32s (List.map (triple32 -1I) [0 .. 32])
checkEq "BigInt.ToInt64 positives" positive64s (List.map (triple64  1I) [0 .. 64])
checkEq "BigInt.ToInt64 positives" negative64s (List.map (triple64 -1I) [0 .. 64])
;;


(* END *)  


#if ALL_IN_ONE
let RUN() = !failures
#else
let aa =
  match !failures with 
  | [] -> 
      stdout.WriteLine "Test Passed"
      System.IO.File.WriteAllText("test.ok","ok")
      exit 0
  | _ -> 
      stdout.WriteLine "Test Failed"
      exit 1
#endif


