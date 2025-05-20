
let
  pkgs = import <nixpkgs> {};
in
pkgs.buildFHSUserEnv {
  name = "rider-dotnet-env";
  targetPkgs = pkgs: with pkgs; [
    dotnet-sdk_9
    mono
    zlib
    icu
    openssl
    curl
    libuuid
    libunwind
    lttng-ust
    krb5
    glib
    dbus
    xorg.libX11
    xorg.libXcursor
    xorg.libXrandr
    xorg.libXext
  ];
  profile = ''
    export DOTNET_ROOT=${pkgs.dotnet-sdk_9}
    export PATH=$DOTNET_ROOT:$PATH
  '';
  runScript = "rider";
}

