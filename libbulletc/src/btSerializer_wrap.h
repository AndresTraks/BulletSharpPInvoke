#include "main.h"

#ifndef BT_SERIALIZER_H
#define p_btSerializer_Allocate void*
#define p_btSerializer_FinalizeChunk void*
#define p_btSerializer_FindNameForPointer void*
#define p_btSerializer_FindPointer void*
#define p_btSerializer_FinishSerialization void*
#define p_btSerializer_GetBufferPointer void*
#define p_btSerializer_GetChunk void*
#define p_btSerializer_GetCurrentBufferSize void*
#define p_btSerializer_GetNumChunks void*
#define p_btSerializer_GetSerializationFlags void*
#define p_btSerializer_GetUniquePointer void*
#define p_btSerializer_RegisterNameForPointer void*
#define p_btSerializer_SerializeName void*
#define p_btSerializer_SetSerializationFlags void*
#define p_btSerializer_StartSerialization void*
#define btSerializerWrapper void
#else
typedef btChunk* (*p_btSerializer_Allocate)(size_t size, int numElements);
typedef void (*p_btSerializer_FinalizeChunk)(btChunk* chunk, const char* structType,
	int chunkCode, void* oldPtr);
typedef const char* (*p_btSerializer_FindNameForPointer)(const void* ptr);
typedef void* (*p_btSerializer_FindPointer)(void* oldPtr);
typedef void (*p_btSerializer_FinishSerialization)();
typedef const unsigned char* (*p_btSerializer_GetBufferPointer)();
typedef btChunk* (*p_btSerializer_GetChunk)(int chunkIndex);
typedef int (*p_btSerializer_GetCurrentBufferSize)();
typedef int (*p_btSerializer_GetNumChunks)();
typedef int (*p_btSerializer_GetSerializationFlags)();
typedef void* (*p_btSerializer_GetUniquePointer)(void* oldPtr);
typedef void (*p_btSerializer_RegisterNameForPointer)(const void* ptr, const char* name);
typedef void (*p_btSerializer_SerializeName)(const char* ptr);
typedef void (*p_btSerializer_SetSerializationFlags)(int flags);
typedef void (*p_btSerializer_StartSerialization)();

class btSerializerWrapper : public btSerializer
{
private:
	p_btSerializer_Allocate _allocateCallback;
	p_btSerializer_FinalizeChunk _finalizeChunkCallback;
	p_btSerializer_FindNameForPointer _findNameForPointerCallback;
	p_btSerializer_FindPointer _findPointerCallback;
	p_btSerializer_FinishSerialization _finishSerializationCallback;
	p_btSerializer_GetBufferPointer _getBufferPointerCallback;
	p_btSerializer_GetChunk _getChunkCallback;
	p_btSerializer_GetCurrentBufferSize _getCurrentBufferSizeCallback;
	p_btSerializer_GetNumChunks _getNumChunksCallback;
	p_btSerializer_GetSerializationFlags _getSerializationFlagsCallback;
	p_btSerializer_GetUniquePointer _getUniquePointerCallback;
	p_btSerializer_RegisterNameForPointer _registerNameForPointerCallback;
	p_btSerializer_SerializeName _serializeNameCallback;
	p_btSerializer_SetSerializationFlags _setSerializationFlagsCallback;
	p_btSerializer_StartSerialization _startSerializationCallback;

public:
	btSerializerWrapper(p_btSerializer_Allocate allocateCallback, p_btSerializer_FinalizeChunk finalizeChunkCallback,
		p_btSerializer_FindNameForPointer findNameForPointerCallback, p_btSerializer_FindPointer findPointerCallback,
		p_btSerializer_FinishSerialization finishSerializationCallback, p_btSerializer_GetBufferPointer getBufferPointerCallback,
		p_btSerializer_GetChunk getChunkCallback, p_btSerializer_GetCurrentBufferSize getCurrentBufferSizeCallback,
		p_btSerializer_GetNumChunks getNumChunksCallback, p_btSerializer_GetSerializationFlags getSerializationFlagsCallback,
		p_btSerializer_GetUniquePointer getUniquePointerCallback, p_btSerializer_RegisterNameForPointer registerNameForPointerCallback,
		p_btSerializer_SerializeName serializeNameCallback, p_btSerializer_SetSerializationFlags setSerializationFlagsCallback,
		p_btSerializer_StartSerialization startSerializationCallback);

	virtual btChunk* allocate(size_t size, int numElements);
	virtual void finalizeChunk(btChunk* chunk, const char* structType, int chunkCode,
		void* oldPtr);
	virtual const char* findNameForPointer(const void* ptr) const;
	virtual void* findPointer(void* oldPtr);
	virtual void finishSerialization();
	virtual const unsigned char* getBufferPointer() const;
	virtual btChunk* getChunk(int chunkIndex) const;
	virtual int getCurrentBufferSize() const;
	virtual int getNumChunks() const;
	virtual int getSerializationFlags() const;
	virtual void* getUniquePointer(void* oldPtr);
	virtual void registerNameForPointer(const void* ptr, const char* name);
	virtual void serializeName(const char* ptr);
	virtual void setSerializationFlags(int flags);
	virtual void startSerialization();
};
#endif

extern "C"
{
	EXPORT btChunk* btChunk_new();
	EXPORT int btChunk_getChunkCode(btChunk* obj);
	EXPORT int btChunk_getDna_nr(btChunk* obj);
	EXPORT int btChunk_getLength(btChunk* obj);
	EXPORT int btChunk_getNumber(btChunk* obj);
	EXPORT void* btChunk_getOldPtr(btChunk* obj);
	EXPORT void btChunk_setChunkCode(btChunk* obj, int value);
	EXPORT void btChunk_setDna_nr(btChunk* obj, int value);
	EXPORT void btChunk_setLength(btChunk* obj, int value);
	EXPORT void btChunk_setNumber(btChunk* obj, int value);
	EXPORT void btChunk_setOldPtr(btChunk* obj, void* value);
	EXPORT void btChunk_delete(btChunk* obj);

	EXPORT btSerializerWrapper* btSerializerWrapper_new(p_btSerializer_Allocate allocateCallback,
		p_btSerializer_FinalizeChunk finalizeChunkCallback, p_btSerializer_FindNameForPointer findNameForPointerCallback,
		p_btSerializer_FindPointer findPointerCallback, p_btSerializer_FinishSerialization finishSerializationCallback,
		p_btSerializer_GetBufferPointer getBufferPointerCallback, p_btSerializer_GetChunk getChunkCallback,
		p_btSerializer_GetCurrentBufferSize getCurrentBufferSizeCallback, p_btSerializer_GetNumChunks getNumChunksCallback,
		p_btSerializer_GetSerializationFlags getSerializationFlagsCallback, p_btSerializer_GetUniquePointer getUniquePointerCallback,
		p_btSerializer_RegisterNameForPointer registerNameForPointerCallback, p_btSerializer_SerializeName serializeNameCallback,
		p_btSerializer_SetSerializationFlags setSerializationFlagsCallback, p_btSerializer_StartSerialization startSerializationCallback);

	EXPORT void btSerializer_delete(btSerializer* obj);

	EXPORT btDefaultSerializer* btDefaultSerializer_new();
	EXPORT btDefaultSerializer* btDefaultSerializer_new2(int totalSize);
	EXPORT unsigned char* btDefaultSerializer_internalAlloc(btDefaultSerializer* obj, size_t size);
	EXPORT void btDefaultSerializer_writeHeader(btDefaultSerializer* obj, unsigned char* buffer);

	EXPORT char* getBulletDNAstr();
	EXPORT int getBulletDNAlen();
	EXPORT char* getBulletDNAstr64();
	EXPORT int getBulletDNAlen64();
}
