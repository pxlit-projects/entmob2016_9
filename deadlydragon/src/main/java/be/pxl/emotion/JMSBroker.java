package be.pxl.emotion;

import be.pxl.emotion.services.MessageProducer;
import org.apache.activemq.broker.BrokerFactory;
import org.apache.activemq.broker.BrokerService;
import org.springframework.context.Lifecycle;

import java.net.URI;
import java.net.URISyntaxException;

/**
 * Created by Dragonites on 22/11/2016.
 */
public class JMSBroker {
    public static void main(String[] args) throws URISyntaxException, Exception {
        BrokerService broker = BrokerFactory.createBroker(new URI("broker:(tcp://0.0.0.0:61616)"));
        broker.start();
        System.in.read();
        broker.stop();
    }
}