const fs = require('fs');
const path = require('path');
const { MongoClient } = require('mongodb');

const IMAGES_ROOT = path.join(__dirname, 'assets', 'img', 'propertyImgs');
const MONGO = 'mongodb://localhost:27017';
const DB = 'realestate';
const COLL = 'propertyImages';

(async () => {
  const client = new MongoClient(MONGO, { useNewUrlParser: true, useUnifiedTopology: true });
  try {
    await client.connect();
    console.log('Connected to MongoDB');
    const coll = client.db(DB).collection(COLL);

    const propertyFolders = fs.readdirSync(IMAGES_ROOT, { withFileTypes: true })
      .filter(d => d.isDirectory())
      .map(d => d.name);

    for (const propId of propertyFolders) {
      const folder = path.join(IMAGES_ROOT, propId);
      const files = fs.readdirSync(folder).filter(f => /\.(jpe?g|png|webp|gif)$/i.test(f));
      for (let i = 0; i < files.length; i++) {
        const file = files[i];
        const idImage = `pi_${propId}_${i+1}`;
        const exists = await coll.findOne({ IdProperty: propId, File: file });
        if (!exists) {
          const doc = {
            IdPropertyImage: idImage,
            IdProperty: propId,
            File: file,
            Enabled: true,
            CreatedAt: new Date()
          };
          await coll.insertOne(doc);
          console.log('Inserted', idImage, file);
        } else {
          console.log('Skipped (exists)', propId, file);
        }
      }
    }
    console.log('Done');
  } catch (err) {
    console.error('Error', err);
  } finally {
    await client.close();
  }
})();