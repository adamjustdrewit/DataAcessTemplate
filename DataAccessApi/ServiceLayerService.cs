namespace DataAccessApi
{
    class ServiceLayerService
    {
        //private  persistentStorage;

        //public ServiceLayerService()
        //{
        //    persistentStorage = new MongoFirmPersistentStorage();
        //}

        /// Ok, so imagine this is the service layer, we have some sexy (non-anemic) entities with properties and behaviour and shit
        /// and that's all golden, but we need this seperate shit (that the entity/business layer has no business knowing about) that
        /// should just magically be able to persist stuff. We couldn't give fewer fucks in this layer how that happens, it could
        /// be using a 3d printer to create silicon moses tablets for all we care.
        /// 
        /// Anyway, this layer kind of composes all that shit together so I'm just going to make some stuff up in terms of how this should
        /// look from a client callers perspective.
        

        //public Client[] GetFirms(int customerId)
        //{
        //    /// So imagine that some presentation layer wants some clients
        //    /// It's rare that you'll ever not segment by a customer Id or something so there's one of them too
        //    var repo = persistentStorage.FirmRepo;

        //    // wonder if we could do something in the baseservice so whenever you return an IEnumerable from a function with a return of
        //    // an array it can just convert it for you to reduce the amount of ToArray() dotted around.

        //    // Also will need some DTO mapping magic here and in future functions
        //    return repo.Entities.ToArray();
        //}

        //public Client GetClientById(int firmId)
        //{
        //    /// Now that we have 2 calls to the same repo I would refactor this out so clientRepo
        //    /// is a private member of the service, meaning that whenever this class is instantiated,
        //    /// there will be a sexy clientRepo ready to use, with the caveat that the ctor would need
        //    /// the customer parameter passed in

        //    // It would be nice to be able to treat the repos as dictionaries too, seeing as the have motherfucking IDs
        //    return persistentStorage.FirmRepo[firmId];
        //}

        //public void UpdateClient(Client updatedFirm)
        //{
        //    var firm = persistentStorage.FirmRepo[updatedFirm.Id];
        //    firm.FirmName = updatedFirm.ClientName;
        //    //... other updates to fields

        //    // Once we're happy you should just be able to
        //    persistentStorage.Submit();
        //}

        /// Ok, looks good so far, but what about nesting
        /// Well, let's try some addresses
        //public AddressDTO[] GetAddresses(int clientId)
        //{
        //    // So here's the thing about addresses... They're value objects, which means that if one
        //    // address has the same properties as another address, then they're the same address and should have the
        //    // same hash. This also means we can get rid of Ids all together. Fuck yeah
        //    // Also, nested shit should look like this (lazy loaded though of course)
        //    return clientRepo[clientId].Addresses;
        //}

        /// Well shit son, that's pretty hot, but what if we need to update addresses. Well in an ideal world we
        /// shouldn't have to worry about additional shit on top of the standard stuff and just let whatever context is hiding in that
        /// repo figure it out for us so:
        //public void AddAddress(int clientId, AddressDTO address)
        //{
        //    var client = clientRepo[clientId];
            
        //    client.Addresses.Add(AddressDTO);

        //    // As part of the submit in the repo, the IEntity will expose a Validate function that will throw exceptions as needed
        //    persistentStorage.Submit();
        //}

        // Fuck it, you could just do it all at once
        //public void UpdateAddresses(ClientDTO dto)
        //{
        //    var client = clientRepo[clientId];
        //    client.Addresses = dto.Addresses;

        //    persistentStorage.Submit();
        //}

        /// So there you go. The basic idea is that you just fuck around with the root aggregate pretending it's in memory, then when you want to 
        /// submit it you talk to the persistentStorage to get it done.
        /// 
        /// The persistent storage also allows us to do multiple things under the same submit, to potentially multiple repos (e.g. persistentStorage.Users), it's like a not shit unit of work pattern
        /// plus making the repos strongly typed to the correct repository interface, so we'll know all the additional shit the repos can do without having to cast them... E.g        
        //public Client[] GetTopTenClients()
        //{
        //    return persistentStorage.FirmRepo.GetTopClients(10).ToArray();
        //}

    }
}
