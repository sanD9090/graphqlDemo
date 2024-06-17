using GraphqlDemo.DTOs;
using GraphqlDemo.Services.Instructors;

namespace GraphqlDemo.DataLoader;

public class InstructorDataLoader:BatchDataLoader<Guid,InstructorDTO>
{
    private readonly InstructorRepository _instructorRepository;
    
    
    public InstructorDataLoader(InstructorRepository instructorsRepository,IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _instructorRepository = instructorsRepository;

    }

    protected override async Task<IReadOnlyDictionary<Guid, InstructorDTO>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        try
        {

        IEnumerable<InstructorDTO> instructors = await _instructorRepository.GetManyByIds(keys);
        
        return instructors.ToDictionary(instructor => instructor.Id);
        
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}