# #wait for the SQL Server to come up
# sleep 30s

# echo "running set up script"
# #run the setup script to create the DB and the schema in the DB
# /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd -d master -i db-init.sql

#!/bin/bash

SA_PASSWORD=P@ssw0rd
TIMEOUT=60
DBSTATUS=1
ERRCODE=1
i=0

while [[ $i -lt $TIMEOUT ]] ; do
	i=$i+1
	DBSTATUS=$(/opt/mssql-tools/bin/sqlcmd -h -1 -t 1 -U sa -P $SA_PASSWORD -Q "SET NOCOUNT ON; Select SUM(state) from sys.databases")
	ERRCODE=$?
	sleep 1

	if [[ $DBSTATUS -eq 0 ]] && [[ $ERRCODE -eq 0 ]]; then
		break
	fi
done

if [[ $DBSTATUS -ne 0 ]] || [[ $ERRCODE -ne 0 ]]; then
	echo "SETUP: SQL Server took more than $TIMEOUT seconds to start up or one or more databases are not in an ONLINE state"
	exit 1
fi

sleep 2

# Run the setup script to create the DB and the schema in the DB
/opt/mssql-tools/bin/sqlcmd -S sql-server -U sa -P $SA_PASSWORD -d master -i db-init.sql