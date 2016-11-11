package be.pxl.emotion.repositories;

import be.pxl.emotion.beans.Action;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

/**
 * Created by Dragonites on 11/11/2016.
 */

@Repository
public interface ActionRepository extends CrudRepository<Action, Integer> {
}
