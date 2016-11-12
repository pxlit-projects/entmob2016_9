package be.pxl.emotion.repositories;

import be.pxl.emotion.beans.Profile;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * Created by Dragonites on 11/11/2016.
 */

@Repository
public interface ProfileRepository extends CrudRepository<Profile,Integer> {
    List<Profile> findByUserId(int userId);
}
