package com.turncount.zanyzebra.repositories;

import com.turncount.zanyzebra.entities.Action;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface ActionRepository extends CrudRepository<Action, Integer> {
}
