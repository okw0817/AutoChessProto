using UnityEngine;
using System.Collections.Generic;

public class ProjectileController : MonoBehaviour
{
    #region Members : Private
    Dictionary<string, List<Projectile>> projectile_Dic = new Dictionary<string, List<Projectile>>();
    List<Projectile> deleteProjectile = new List<Projectile>();
    List<Projectile> addProjectile = new List<Projectile>();
    #endregion

    #region Methods : Private
    private void AddProjectileDic(Projectile projectile)
    {
        if (projectile_Dic.ContainsKey(projectile.ProjectileName))
        {
            projectile_Dic[projectile.ProjectileName].Add(projectile);
        }
        else
        {
            projectile_Dic.Add(projectile.ProjectileName, new List<Projectile>());
            projectile_Dic[projectile.ProjectileName].Add(projectile);
        }
    }

    private void DeleteProjectileDic(Projectile projectile)
    {
        if (projectile_Dic.ContainsKey(projectile.ProjectileName))
        {
            projectile_Dic[projectile.ProjectileName].Remove(projectile);

            if (projectile_Dic[projectile.ProjectileName].Count == 0)
                projectile_Dic.Remove(projectile.ProjectileName);
        }
    }
    #endregion

    #region Methods : Public
    public void AddProjectile(Projectile projectile)
    {
        addProjectile.Add(projectile);
    }

    public void DeleteProjectile(Projectile projectile)
    {
        deleteProjectile.Add(projectile);
    }

    public void UpdateProjectileList()
    {
        foreach(var projectile in addProjectile)
        {
            AddProjectileDic(projectile);
        }

        foreach (var projectile in deleteProjectile)
        {
            DeleteProjectileDic(projectile);
        }

        if(addProjectile.Count != 0)
            addProjectile.Clear();

        if (deleteProjectile.Count != 0)
            deleteProjectile.Clear();
    }

    public void UpdateMove()
    {
        if (projectile_Dic.Count == 0)
            return;

        var projectileEnumerator = projectile_Dic.GetEnumerator();
        while (projectileEnumerator.MoveNext())
        {
            if (projectileEnumerator.Current.Value.Count == 0)
                continue;

            var listEnumerator = projectileEnumerator.Current.Value.GetEnumerator();
            while (listEnumerator.MoveNext())
            {
                if (!listEnumerator.Current.gameObject.activeSelf)
                    break;

                listEnumerator.Current.Move();
            }
        }
    }
    #endregion
}
