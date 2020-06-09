# Feature flags: the toggle, the A/B test and the canary

### The beauty of a feature flag is simplicity - it's a conditional block of code that chooses between different execution paths at runtime.

In this talk, we will combine this simple concept with DevOps principles to perform powerful operations like separating code deployments from feature releases, canary releases, product owner-driven releases, testing in production, beta & A/B testing and even, kill switches. The demos and code samples will use open source libraries like .NET Core FeatureManagement (C#) and SaaS products like Launch Darkly and Azure App Configuration. Learn how to achieve control and mastery over your deployments and releases with feature flags.

1. [What are Feature Flags?](#1)
2. Differences between deployment and release
3. Feature Flag Frameworks
    - Sim
4. Separate deployments from releases and test in production with:
    - Toggles/Kill Switches
    - A/B Testing
    - Canary
5. Tech Debt


### 1. What are Feature Flags? {#1}
>Feature Toggles (often also referred to as Feature Flags) are a powerful technique, allowing teams to modify system behavior without changing code.
-Martin Fowler
https://martinfowler.com/articles/feature-toggles.html

In software, a flag is "one or more bits used to store binary values" aka a Boolean that can either be true or false. In the same context in software, a feature is a chunk of functionality that delivers some kind of value.

Thus, a feature flag, in the simplest terms, is an if statement surrounding some chunk of functionality in your software. Feature flags, in reality, can be and are more complex than that. A feature flag is a way to change your software’s functionality without changing and re-deploying your code.

Feature Flags, also referred to as "Feature Toggles" is a set of patterns which can help a team to deliver new functionality to users rapidly but safely. 
