# Contributing to MelodyMine

We are thrilled that you are interested in contributing to the MelodyMine project! This document outlines the process for contributing changes and the guidelines to follow.

## Branching Strategy

Our project uses two main branches: `main` for releases and `develop` for development.

### Steps for Contributing:

1. **Starting with `main`**: The `main` branch is where the source code of HEAD always reflects a production-ready state.

2. **Creating `develop`**: The `develop` branch is created from the `main` branch. It serves as an integration branch for features and fixes. It is not directly used for releases but rather to prepare the code for the next release.

3. **Feature Branches**: All feature development should take place in a dedicated branch based on the `develop` branch. Please use a clear and descriptive name for your feature branch, like `feature/add-search-functionality`.

    - To create a feature branch:
      ```bash
      git checkout develop
      git checkout -b feature/your_feature_name
      ```

4. **Making Changes**: Make your changes on your feature branch and commit them. Make sure your commits clearly describe the change you are making and why.

5. **Ready for Merge**: Once your feature is complete, create a pull request to merge your feature branch into `develop`.

    - Before submitting a pull request, make sure to merge the latest `develop` into your feature branch and resolve any conflicts.

6. **Pull Request Review**: Your pull request will be reviewed by maintainers and may require changes before it can be merged.

7. **Merge into `develop`**: After your pull request is approved, it will be merged into `develop`.

8. **Releases**: When we decide it's time to make a release, we will prepare the `develop` branch for this, merging it into `main`. Once merged, we will tag the release with a version number.

## General Guidelines

- Ensure that your code adheres to the existing style of the project to maintain consistency.
- Write detailed commit messages to help reviewers understand your changes.
- Update the README.md with details of changes to the interface or significant architecture, if necessary.
- Increase the version numbers in any examples files and the README.md to the new version that this Pull Request would represent.

## Pull Request Process

1. Ensure any install or build dependencies are removed before the end of the layer when doing a build.
2. Update the README.md with details of changes to the interface, this includes new environment variables, exposed ports, useful file locations, and container parameters.
3. You may merge the Pull Request in once you have the sign-off of two other developers, or if you do not have permission to do that, you may request the second reviewer to merge it for you.

Thank you for taking the time to contribute to MelodyMine!
