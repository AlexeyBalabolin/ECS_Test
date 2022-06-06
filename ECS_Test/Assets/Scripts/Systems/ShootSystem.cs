using Unity.Entities;

public class ShootSystem : ComponentSystem
{
    private EntityQuery _shootQuery;

    protected override void OnCreate()
    {
        _shootQuery = GetEntityQuery(ComponentType.ReadOnly<ShootDataComponent>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_shootQuery).ForEach((Entity entity, ShootDataComponent shootData,ref InputData inputData) =>
        {
            if (shootData.ShootAction is IAbility shootAbility)
            {
                if(inputData.IsShoot)
                    shootAbility.Execute();
            }
        });
    }
}
