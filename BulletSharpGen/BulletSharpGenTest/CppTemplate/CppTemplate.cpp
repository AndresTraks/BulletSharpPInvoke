#include "CppTemplate.h"

template<typename T>
TemplateBase<T>::TemplateBase(T value)
{
	this->value = value;
}

template<typename T>
T TemplateBase<T>::getValue()
{
	return value;
}


template<typename T>
Template<T>::Template(T value)
	: TemplateBase(value)
{
}


TemplateSpecialization::TemplateSpecialization()
	: Template<int>(1)
{
}
