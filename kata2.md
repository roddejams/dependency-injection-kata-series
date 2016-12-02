# Kata 2 - Using DI containers

Although dependency injection containers are optional, they help to enforce the practice of dependency injection in the codebase. Whereas DI is a technique, DI containers are a technology to make things easier.

Sometimes it is scary to choose a DI container because there is a risk that you've made the wrong decision and you will be stuck with your bad choice for the lifetime of the application. By limiting the use of the container to the application's composition root, we can isolate the use of the DI library to the bootstrap code.


**We are going to use Autofac in our examples, but you are welcome to use your favourite DI container**.

## Task 1 - Add a DI library

Install Autofac using NuGet and add a reference into the console application.

Create an empty `ContainerBuilder` and call `builder.Build()` to build your first `IContainer`.

## Task 2 - Register all your dependencies

After the first kata your entry point should contain all the object construction in your application.

We will now replace dependency construction using `new` with a single call to `container.Resolve`.

You can replace classes one-by-one with a process like this:

1. Find a class with no dependencies or dependencies already registered
2. Register that class with Autofac
3. Replace its usages with `container.Resolve`
4. REPEAT until you resolve the top level class

_For this exercise, avoid using auto-wiring as that will appear in a future Kata._

## Task 3 - Extract a module

Modules are a way to group related DI registrations together.

Test systems can have their own modules too, often you will want to override or swap out a module with a test module that simplifies test running.

Here is an example Autofac module:

```
public class CarTransportModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.Register(c => new Car(c.Resolve<IDriver>())).As<IVehicle>();
    builder.Register(c => new SaneDriver()).As<IDriver>();
  }
}
```

Extract a single module for the entire app.

## Task 4 - Make it easier to add square painters

At the moment, if you add a new square painter, then you have to add a new `case` statement to handle it -- assuming you kept the `GetSquarePainter` factory method. In addition, the object is being constructed manually instead of being constructed by the DI container.

Ideally, we want to only have to register the new square painter and then it will be accessible throughout the entire application. However, you still need to select the appropriate square painter based on the provided pattern. How can DI containers help with this?

We want you to adapt the code so that adding a new square painter is a maximum one-line change -- inlining the `case` statement is not an option!

