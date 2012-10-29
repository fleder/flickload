using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FlickrNet;

namespace FlickrWPF_Test
{
    //<summary>
    // This class implements a fake FlickrNet collection collection
    // It looks like this:
    // Collection A 
    //             Collection A.A
    //                           PhotoSet 1 (ps1)
    //             Collection A.B
    // Collection B 
    //             PhotoSet 2 (ps2)
    class FakeCollectionCollection
    {

        public FlickrNet.CollectionCollection fake_collection;

        public FakeCollectionCollection()
        {
            fake_collection = new CollectionCollection();

            // ------------- Highest level -----------
            FlickrNet.Collection colA = new Collection();
            colA.Title = "Collection A";
            colA.CollectionId = "A";

            FlickrNet.Collection colB = new Collection();
            colB.Title = "Collection B";
            colB.CollectionId = "B";

            fake_collection.Add(colA);
            fake_collection.Add(colB);
            
            // ------------ Second level --------------
            FlickrNet.Collection colAA = new Collection();
            colAA.Title = "Collection A.A";
            colAA.CollectionId = "A.A";

            FlickrNet.Collection colAB = new Collection();
            colAB.Title = "Collection A.B";
            colAB.CollectionId = "A.B";

            colA.Collections.Add(colAA);
            colA.Collections.Add(colAB);

            // ------------ Photo sets ---------------
            CollectionSet ps1 = new CollectionSet();            
            ps1.SetId = "ps1";
            ps1.Title = "PhotoSet 1";

            CollectionSet ps2 = new CollectionSet();
            ps2.SetId = "ps2";
            ps2.Title = "PhotoSet 2";

            colAA.Sets.Add(ps1);
            colB.Sets.Add(ps2);
        }
        
    }
}
