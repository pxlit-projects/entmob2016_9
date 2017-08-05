package com.turncount.zanyzebra.repositories;

import com.turncount.zanyzebra.entities.Command;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface CommandRepository extends CrudRepository<Command,Integer> {
}
