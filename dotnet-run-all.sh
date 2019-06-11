#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
PREFIX=TrashRouting
REPOSITORIES=($PREFIX.API $PREFIX.Cluster $PREFIX.Routes $PREFIX.Sync)

for REPOSITORY in ${REPOSITORIES[*]}
do
	 echo ========================================================
	 echo Starting a service: $REPOSITORY
	 echo ========================================================
     cd $REPOSITORY
     dotnet run &
     cd ..
done