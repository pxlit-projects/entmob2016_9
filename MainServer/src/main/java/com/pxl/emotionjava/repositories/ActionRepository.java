package com.pxl.emotionjava.repositories;

import com.pxl.emotionjava.entities.Action;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface ActionRepository extends CrudRepository<Action, Integer> {
}
