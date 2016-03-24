class Base
{

};

template<typename T>
class TemplateBase : Base
{
	T value;

public:
	TemplateBase(T value);

	T getValue();
};

template<typename T>
class Template : TemplateBase<T>
{
public:
	Template(T value);
};

class TemplateSpecialization : Template<int>
{
public:
	TemplateSpecialization();
};
