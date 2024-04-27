/*
 * Author: Sakthi Santhosh
 * Created on: 24/04/2024
 */
using Challenge1.Library.Exceptions;
using Challenge1.Library.Models;
using Challenge1.Library.Repositories;

namespace Challenge1.Tests.Repositories;

class DummyModel : BaseModel
{
}

class DummyRepository : BaseRepository<DummyModel>
{
}

[TestFixture]
public class TestBaseRepository
{
    private DummyRepository _dummyRepository;
    private DummyModel _dummyModel;

    [SetUp]
    public void Setup()
    {
        _dummyRepository = new DummyRepository();
        _dummyModel = new DummyModel();
    }

    [Test]
    public void TestAddShouldAddModel()
    {
        var addedModel = _dummyRepository.Add(_dummyModel);

        Assert.Multiple(() =>
        {
            Assert.That(_dummyRepository.GetCount(), Is.EqualTo(1));
            Assert.That(addedModel.Id, Is.EqualTo(1));
        });

    }

    [Test]
    public void TestGetByIdShouldReturnModel()
    {
        var addedModel = _dummyRepository.Add(_dummyModel);
        var retrievedModel = _dummyRepository.GetById(1);

        Assert.That(retrievedModel.Id, Is.EqualTo(addedModel.Id));
    }

    [Test]
    public void TestGetByIdInvalidIdShouldThrowModelEntityNotFoundException()
    {
        var exception = Assert.Throws<ModelEntityNotFoundException>(() => _dummyRepository.GetById(999));

        Assert.That(exception.Message, Is.EqualTo("No entity with the ID is found."));
    }

    [Test]
    public void TestUpdateShouldUpdateModel()
    {
        var addedModel = _dummyRepository.Add(_dummyModel);
        var updatedModel = _dummyRepository.GetById(1);

        addedModel.IsActive = false;
        _dummyRepository.Update(addedModel);

        Assert.That(updatedModel.IsActive, Is.False);
    }

    [Test] // TODO
    public void TestDeleteShouldRemoveModel()
    {
        var addedModel = _dummyRepository.Add(_dummyModel);

        _dummyRepository.Delete(addedModel);
        Assert.That(_dummyRepository.GetCount(), Is.EqualTo(0));
    }

    [Test]
    public void TestDeleteInvalidModelShouldThrowModelEntityNotFoundException()
    {
        var exception = Assert.Throws<ModelEntityNotFoundException>(() => _dummyRepository.Delete(_dummyModel));

        Assert.That(exception.Message, Is.EqualTo("No entity with the ID is found."));
    }

    [Test]
    public void TestIndexerGetValidIndexReturnsModel()
    {
        _dummyRepository.Add(_dummyModel);

        var model = _dummyRepository[0];

        Assert.That(model.Id, Is.EqualTo(1));
    }

    [Test]
    public void TestIndexerSetValidIndexSetsModel()
    {
        var newModel = new DummyModel();

        _dummyRepository.Add(_dummyModel);
        _dummyRepository[0] = newModel;

        Assert.That(_dummyRepository[0], Is.EqualTo(newModel));
    }

    [Test]
    public void TestIndexerGetInvalidIndexThrowsIndexOutOfRangeException()
    {
        Assert.Throws<IndexOutOfRangeException>(() => { var data = _dummyRepository[999]; });
    }

    [Test]
    public void TestIndexerSetInvalidIndexThrowsIndexOutOfRangeException()
    {
        var exception = Assert.Throws<IndexOutOfRangeException>(() => _dummyRepository[999] = new DummyModel());

        Assert.That(exception.Message, Is.EqualTo("Index was outside the bounds of the array."));
    }
}
