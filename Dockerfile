# Note: Since git repositories are cloned, an active internet connection is required.
# This Dockerfile mainly uses the Dockerfile from the SPL Conqueror repository.

# The predictions were performed on Debian 9 (stretch)
FROM debian:stretch

# Set the working directory to /app
WORKDIR /application

RUN apt update

# Add mono package repository and update repositories
RUN apt install -y -qq apt-transport-https dirmngr gnupg ca-certificates \
    && apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF \
    && echo "deb https://download.mono-project.com/repo/debian stable-stretch main" | tee /etc/apt/sources.list.d/mono-official-stable.list \
    && apt update

# Install git and wget
RUN apt install -y -qq git wget unzip mono-complete mono-devel nuget

# Download the whole repository including SPL Conqueror and build it
RUN git clone --depth=1 https://github.com/se-passau/VariabilityExtraction-SupplementaryWebsite.git \
    && cd VariabilityExtraction-SupplementaryWebsite \
    && git submodule update --init --recursive \
    && cd SPLConqueror_Dune/SPLConqueror/SPLConqueror/SPLConqueror \
    && msbuild /p:Configuration=Release /p:TargetFrameworkVersion="v4.5" /p:TargetFrameworkProfile="" ./SPLConqueror_Core.csproj \
    && cd ../../../DuneAnalyzer \
    && nuget restore ./ \
    && msbuild /p:Configuration=Release /p:TargetFrameworkVersion="v4.5" /p:TargetFrameworkProfile="" /p:ReferencePath=/application/VariabilityExtraction-SupplementaryWebsite/SPLConqueror_Dune/SPLConqueror/SPLConqueror/SPLConqueror/bin/Release/ ./DuneAnalyzer.sln \
    && cd .. \
    && mkdir DebugOutput \
    && mkdir Results_linearsolver \
    && mkdir Results_ellipticproblem

# Execute VORM on the linearsolver
RUN cd /application/VariabilityExtraction-SupplementaryWebsite/SPLConqueror_Dune/ \
    && mono /application/VariabilityExtraction-SupplementaryWebsite/SPLConqueror_Dune/DuneAnalyzer/DuneAnalyzer/bin/Release/DuneAnalyzer.exe /application/VariabilityExtraction-SupplementaryWebsite/SPLConqueror_Dune/linearsolver.txt; exit 0

# Now, run the VORM on the ellipticproblem
RUN cd /application/VariabilityExtraction-SupplementaryWebsite/SPLConqueror_Dune/ \
    && mono /application/VariabilityExtraction-SupplementaryWebsite/SPLConqueror_Dune/DuneAnalyzer/DuneAnalyzer/bin/Release/DuneAnalyzer.exe /application/VariabilityExtraction-SupplementaryWebsite/SPLConqueror_Dune/ellipticproblem.txt; exit 0