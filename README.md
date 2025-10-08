## Launch Local Database and Load Fixtures

1. (Optional) Launch Mongo in Docker:
docker-compose -f docker/docker-compose.yml up -d

2. Manual export/import with mongoimport:
mongoimport --uri="mongodb://localhost:27017/realestate" --collection=owners --file=db/fixtures/realestate.owners.json --jsonArray --drop
mongoimport --uri="mongodb://localhost:27017/realestate" --collection=properties --file=db/fixtures/realestate.properties.json --jsonArray --drop
mongoimport --uri="mongodb://localhost:27017/realestate" --collection=propertyImage --file=db/fixtures/realestate.propertyImage.json --jsonArray --drop
mongoimport --uri="mongodb://localhost:27017/realestate" --collection=propertyTraces --file=db/fixtures/realestate.propertyTraces.json --jsonArray --drop

3. Helper Script (Unix):
./scripts/import-fixtures.sh
